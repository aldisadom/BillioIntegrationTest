# BillioIntegrationTest

End-to-end tests for the Biller API: TUnit tests that call a running Biller stack over HTTP.

## Running
- Start Biller first: `docker compose up -d --build` in `../Biller` (API at http://localhost:8090).
- `dotnet test BillioIntegrationTest.sln`
- The tests delete **all** data through the API before and after the run (`Tests/TestPreparation.cs`).
  Never point them at production.
- CI clones the Biller branch with the same name as this branch, or Biller master if there isn't
  one. Biller's CI does the same the other way round, so an API change and its tests go on branches
  with the same name.

## How the tests are built
- One file per resource in `src/IntegrationTests/Tests/`. They run as one chain with `[DependsOn]`:
  users → sellers → customers → items → invoices.
- Test data comes from `<Resource>TestDataSources` through `[MethodDataSource]`, as `TestCaseModel<T>`
  or `TestCaseError<T>` with a `TestName` that shows in the output.
- Entities created by one test are kept in the `Saved<Resource>s` dictionaries for later tests.
- Error cases check the status code, message and extended message (`ErrorModel`).
- The HTTP clients are local copies in `src/IntegrationTests/Clients/` (base URL `http://localhost:8090`)
  returning `Result<T>`. Request and response types come from the `BillerContracts` NuGet package.

## Conventions
- Expected values come from the request, the spec, or data the test set up, never from the response being checked.
- Assert every field of a response, as the existing tests do.
- A new Biller endpoint or rule gets a success test and one test per error.
- No AutoMapper or any other mapping library, in any Biller repo. Build requests by hand.
- Plans for work only in this repo go in `docs/plans/BIL-<n>-<slug>.md` here. Plans that change
  several repos live in Biller's `docs/plans/`.
- Before reading, comparing or using another Biller repo (including the Biller you run against),
  put it on the branch with this branch's name, else on up-to-date master:
  `bash ../Biller/.claude/skills/_sync-repos/scripts/sync-repos.sh --branch "$(git branch --show-current)" Biller BillerContracts BillerFrontEnd`
  (add `--sync` to switch and fast-forward).

## Before a PR
1. Run the tests against the matching Biller branch. Paste the summary line with the passed/failed counts.
2. Run `/code-review` on the branch and tell it to follow `REVIEW.md`. Fix Important findings,
   answer the rest, at most 3 rounds.
3. Fill `.github/pull_request_template.md`.
