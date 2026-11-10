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
- The HTTP clients come from the `BillerContracts.Clients` NuGet package (same version as BillerFrontEnd),
  built once in `ApiClients/TestClients.cs`. URLs are in `appsettings.json` (`ClientsOptions`); environment
  variables such as `ClientsOptions__UserUrl` override them.
- A failing call throws `ClientAPIException`, whose `Error` holds the status code, message and extended
  message. Error tests: `ErrorModel error = (await Assert.ThrowsAsync<ClientAPIException>(() => call))!.Error!;`

## Conventions
- Expected values come from the request, the spec, or data the test set up, never from the response being checked.
- Assert every field of a response, as the existing tests do.
- A new Biller endpoint or rule gets a success test and one test per error.
- Don't read PDF content: no PDF parsing library, no asserts on text, titles or totals inside a PDF.
  A PDF endpoint's test checks the status, `application/pdf`, the file name and the `%PDF-` header.
- No AutoMapper or any other mapping library, in any Biller repo. Build requests by hand.
- Plans for work only in this repo go in `docs/plans/BIL-<n>-<slug>.md` here. Plans that change
  several repos live in Biller's `docs/plans/`.
- Before reading, comparing or using another Biller repo (including the Biller you run against),
  put it on the branch with this branch's name, else on up-to-date master:
  `bash ../Biller/.claude/skills/_sync-repos/scripts/sync-repos.sh --branch "$(git branch --show-current)" Biller BillerContracts BillerFrontEnd`
  (add `--sync` to switch and fast-forward).

## Package limits
Every Biller repo has these limits. Keep them when updating packages.
- Target `net10.0`. A package whose newest version needs a newer .NET stays on the last version that
  supports `net10.0`, until all four repos move .NET together.
- QuestPDF: Community licence only (`LicenseType.Community`). Before an update, check that the
  business still qualifies (under USD 1M revenue, not public sector or publicly traded) and read the
  new version's licence.
- Assertions: AwesomeAssertions (Apache 2.0). Don't add FluentAssertions back: v8+ needs a paid
  licence for commercial use.
- Moq: below 4.20.0, pinned as `[4.18.4,4.20.0)` (4.20.0 added SponsorLink build-time telemetry).
- xUnit: `xunit.v3.mtp-off`, not `xunit.v3`. The .NET 10 SDK's `dotnet test` doesn't run xUnit's
  default Microsoft.Testing.Platform runner through VSTest, and the test scripts rely on VSTest.
- Before taking a new major version, read its release notes and check its licence.
- This repo only: the tests use TUnit (MIT), with its own assertions (`Assert.That(...)`). TUnit runs
  only on Microsoft.Testing.Platform, which the .NET 10 SDK's `dotnet test` won't run through VSTest,
  so `global.json` opts in to the new runner (`"test": { "runner": "Microsoft.Testing.Platform" }`).
  Keep it. With it, TRX output is `--report-trx --report-trx-filename <file>`, not `--logger trx`;
  Biller's CI checks for this file to pick the arguments.

## Before a PR
1. Run the tests against the matching Biller branch. Paste the summary line with the passed/failed counts.
2. Run `/code-review` on the branch and tell it to follow `REVIEW.md`. Fix Important findings,
   answer the rest, at most 3 rounds.
3. Fill `.github/pull_request_template.md`.
