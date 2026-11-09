# Review rules for BillioIntegrationTest

Failure here looks like:
- A test passes whatever the API does: no assert on the result, an expected value taken from the response being checked, or a missing `[DependsOn]` so it runs before its data exists.
- A test leaves data behind, or only passes with data from an earlier run.
- A new test that reads shared data isn't added to the `[DependsOn]` of every later test that changes or adds that data, so the two can run at the same time. Tests run in parallel; only `[DependsOn]` orders them. Check every test whose `[DependsOn]` list already names the neighbouring read tests (in `InvoiceTest.cs`, the tests that wait for `InvoiceGetAllForSeller_PagesOfOne_Success`). Example: the seller paging tests from BIL-102 were missing from 9 such lists and failed in CI once TUnit 1.x scheduled differently (Biller `docs/review-evals.md` row 15).
- An error test checks only the status code and not the message.
- A URL or port is hard-coded in C# code instead of coming from `appsettings.json` (or a `ClientsOptions__*` environment variable).
- A mapping library (AutoMapper or similar) is added.
- A Biller failure mode has no test: a wrong invoice total, a duplicate or skipped invoice number, an old invoice changing after an item is edited (checked through the invoice GET, not the PDF), a password or hash in a response, or one user's data returned to another. Invoices show their current seller, customer and user (BIL-86). There are no tests yet for cross-user access.
- Don't ask for checks on PDF content (text, titles, totals, layout) or a PDF library. PDF endpoints are tested by the response only: status, `application/pdf`, file name, `%PDF-` header, and error cases.

Skip: `bin/`, `obj/`, `TestResults/`.

Tag each finding **Important**, **Nit**, or **Pre-existing** (the bug was there before this change). At most 2 Nits. No naming or formatting comments.

Every finding cites `file:line` and a concrete input that fails. No guesses from names.

Start with the number of Important findings. If there are none, say "No issues found."

When the same kind of bug appears in several places, report it once and list every place.

When a finding repeats across reviews, propose a rule for `CLAUDE.md` or a new test instead of repeating it.
