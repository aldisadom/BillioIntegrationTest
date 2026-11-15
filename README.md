# BillioIntegrationTest

End-to-end tests for the Biller API. They call a running API over HTTP, through the same typed
clients the front end uses, and check each response: users, sellers, series, customers, items and
invoices, including the errors each endpoint returns.

> **The tests delete all data through the API before and after a run.** Never point them at an API
> whose data you want to keep.

## The four repos

| Repo | What it is |
|---|---|
| [Biller](https://github.com/aldisadom/Biller) | The API, the database migrations and the PDFs |
| [BillerContracts](https://github.com/aldisadom/BillerContracts) | Request and response records, enums and typed HTTP clients, published as the `BillerContracts` and `BillerContracts.Clients` NuGet packages |
| [BillerFrontEnd](https://github.com/aldisadom/BillerFrontEnd) | The Blazor UI. It calls the API through `BillerContracts.Clients` |
| [BillioIntegrationTest](https://github.com/aldisadom/BillioIntegrationTest) | Tests that call a running API over HTTP |

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- A running Biller API with a database you can lose
- For the test stack below: Docker, a Biller checkout at `../Biller`, and bash with `curl`,
  `python3` and `flock` (Linux, or WSL on Windows)

## Run

The safe way is Biller's throwaway test stack, which has its own database and ports. From `../Biller`:

```sh
bash .claude/skills/_test-stack/scripts/test-stack.sh up
bash .claude/skills/_test-stack/scripts/run-integration.sh
bash .claude/skills/_test-stack/scripts/test-stack.sh down
```

`run-integration.sh` runs the tests of the folder named `BillioIntegrationTest` next to Biller. For
a checkout in another folder (a worktree), add `--repo` with its path, relative to the folder you
run the command from or absolute, for example `--repo ../BillioIntegrationTest-BIL-93` from
`../Biller`, or it tests the wrong code. To test an API
that isn't the test stack, add `--url http://localhost:<port>`: the script then sets all six
addresses below.

Against an API you started yourself:

```sh
dotnet test BillioIntegrationTest.sln
```

## Configuration

`src/IntegrationTests/appsettings.json` has the API's addresses in `ClientsOptions`, one URL per
resource, by default `http://localhost:8090/...`. An environment variable overrides one URL. Set
all six: one that is left out still points at port 8090, and the tests would delete the data there.
In WSL with the Windows `dotnet.exe`, exported variables don't reach it unless their names are in
`WSLENV`: add `export WSLENV="${WSLENV:+$WSLENV:}ClientsOptions__UserUrl:ClientsOptions__SellerUrl:ClientsOptions__SeriesUrl:ClientsOptions__CustomerUrl:ClientsOptions__ItemUrl:ClientsOptions__InvoiceUrl"`
to the block below. `run-integration.sh` adds it by itself when the dotnet it finds is named
`dotnet.exe`, not when a `dotnet` wrapper points at it.

```sh
export ClientsOptions__UserUrl=http://localhost:8091/users
export ClientsOptions__SellerUrl=http://localhost:8091/sellers
export ClientsOptions__SeriesUrl=http://localhost:8091/series
export ClientsOptions__CustomerUrl=http://localhost:8091/customers
export ClientsOptions__ItemUrl=http://localhost:8091/items
export ClientsOptions__InvoiceUrl=http://localhost:8091/invoices
dotnet test BillioIntegrationTest.sln
```

The tests use TUnit, which runs on Microsoft.Testing.Platform; `global.json` turns that runner on
for `dotnet test`.

## Branches and CI

An API change and its tests go on branches with the same name in Biller and here. CI
(`.github/workflows/ci.yml`) clones the Biller branch with this branch's name, or Biller's master
when there is none, starts it with Docker Compose and runs the tests against it. Biller's CI does
the same the other way round.

## Where things go

- `src/IntegrationTests/Tests/`: one file per resource, with its tests and its test data, plus
  `TestPreparation.cs`, which deletes all data before and after a run, and `Paging.cs`. The tests
  run in parallel; `[DependsOn]` puts a test after the ones whose data it uses.
- `src/IntegrationTests/Models/`: the test case and test data types.
- `src/IntegrationTests/ApiClients/`: the clients and the helper that deletes one resource's rows.

How the tests are built, the conventions and the review rules are in [CLAUDE.md](CLAUDE.md) and
[REVIEW.md](REVIEW.md).

## Licence

MIT, see [LICENSE.txt](LICENSE.txt).
