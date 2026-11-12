# Plan: data sources return Func<T> (TUnit0046)

Jira: [BIL-109](https://aldisadom.atlassian.net/browse/BIL-109). Epic: BIL-89 (Billeris). Repo:
BillioIntegrationTest only, so the plan lives here. Biller doesn't change; CI runs against Biller master.

## Goal

- Every `TestCaseModel<T>` / `TestCaseError<T>` data source returns `IEnumerable<Func<...>>`, so each test
  gets its own copy of its case.
- The build has no TUnit0046 warnings.
- The same tests, case names and expected values. All 219 cases pass, 3 runs in a row.

## Current state (master @ eebb260)

| Where | What |
|---|---|
| Build (.NET SDK 10.0.404, `build.sh BillioIntegrationTest`) | 25 TUnit0046 warnings, one per data source. The full log prints each one twice, which is the 50 in the issue. No other warnings |
| `Models/TestCaseModel.cs` | `TestCaseModel<T>` (`TestName`, `Data`), `TestCaseError<T>` (adds `Error`). Mutable classes; `ToString()` is `TestName`, which the `$testCase` display names use |
| `UserTests.cs` | 4 sources: `AddData`, `AddDataInvalid`, `LoginDataInvalid`, `UpdateDataInvalid` |
| `SellerTests.cs`, `CustomerTests.cs`, `ItemTest.cs`, `SeriesTests.cs` | 3 sources each: `AddData`, `AddDataInvalid`, `UpdateDataInvalid` |
| `InvoiceTest.cs` | 9 sources: `AddData`, `AddDataInvalid`, `UpdateDetailsData`, `UpdateDataInvalid`, `UpdateIdsDataInvalid`, `UpdateStatusData`, `UpdateStatusDataInvalid`, `GenerateData`, `GenerateDataInvalid` |
| String sources | `Emails()`, `Names()`, `InvoiceKeys()`, `UserEmails()` and others return `IEnumerable<string>`. Not flagged; also called directly (`Emails().First()`) |
| Tests that change their case | 7 assignments to `testCase.Error.ExtendedMessage` (Customer 1, Invoice 4, Series 2). Update tests build new requests with `"_new"` names and don't change the case |
| `[DependsOn]` | Names overloads by parameter type, e.g. `[DependsOn(nameof(CustomerAdd_Valid_Success), [typeof(TestCaseModel<CustomerModel>)])]` |
| Retries | No `[Retry]` or `[Repeat]` anywhere |
| Tests | 116 `[Test]` methods. `dotnet test --list-tests` discovers 219 cases with 219 different names |

## Design decisions

1. **Only the return type and `yield return` change.** `IEnumerable<TestCaseModel<X>>` becomes
   `IEnumerable<Func<TestCaseModel<X>>>`, and `yield return new()` becomes `yield return () => new()`.
   The object initializers stay as they are, so the diff is one line per case and `git blame` keeps the data.
2. **Test methods don't change.** TUnit calls the `Func` and passes the `TestCaseModel<X>`, so the
   parameter types, and the `[DependsOn(..., [typeof(...)])]` lists that rely on them, stay. Checked on a
   copy (SDK 10.0 container, before the local SDK was installed): converting Item's 3 sources builds with
   no errors and the warnings go from 25 to 22. The issue's
   scope includes the test methods; they need nothing.
3. **String sources stay `IEnumerable<string>`.** Strings can't be changed, TUnit doesn't flag them, and
   other code calls them directly.
4. **The 7 `testCase.Error.ExtendedMessage = ...` lines stay.** With a fresh case per test they change only
   that test's copy, which is what this issue fixes. Rewriting them is not needed.
5. **No `#pragma` or `NoWarn`.** The warning goes away because the cause does.

## Dependencies

- None. BIL-94 (TUnit 1.81, where TUnit0046 appeared) is Done. No other open branch in this repo.

## Steps

1. [ ] `UserTests.cs`, `SellerTests.cs`, `CustomerTests.cs`, `ItemTest.cs`, `SeriesTests.cs`: 16 sources.
2. [ ] `InvoiceTest.cs`: 9 sources.
3. [ ] Check, with the local .NET SDK 10.0.404:
   - `build.sh BillioIntegrationTest`: 0 TUnit0046, no new warnings.
   - `dotnet test --list-tests` on the branch: the same 219 names as master (saved before the change).
   - The test stack from Biller master, `run-integration.sh --repeat 3`: 219 passed, 3 runs.

## Risk

Test code only. The risk is a renamed or missing case, if TUnit built display names differently for
`Func` sources; the `--list-tests` comparison covers it. Invoice `AddData` reads `DateTime.Now`, which may
now run when the test starts instead of at discovery. It's the same date unless the run crosses midnight,
and the tests read the dates from their own case.

## How to revert

Revert the PR. Nothing else depends on it.

## Out of scope

- New tests.
- Rewriting the in-test `ExtendedMessage` assignments.
- Changes to `TestCaseModel<T>` itself, for example making it immutable.
