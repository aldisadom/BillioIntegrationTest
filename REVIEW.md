# Review rules for BillioIntegrationTest

Failure here looks like:
- A test passes whatever the API does: no assert on the result, an expected value taken from the response being checked, or a missing `[DependsOn]` so it runs before its data exists.
- A test leaves data behind, or only passes with data from an earlier run.
- An error test checks only the status code and not the message.
- A URL or port is hard-coded in C# code instead of coming from `appsettings.json` (or a `ClientsOptions__*` environment variable).
- A mapping library (AutoMapper or similar) is added.
- A Biller failure mode has no test: a wrong invoice total, a duplicate or skipped invoice number, an old invoice changing after master data is edited, a password or hash in a response, or one user's data returned to another. There are no tests yet for PDF generation, invoice status, pagination, or cross-user access.

Skip: `bin/`, `obj/`, `TestResults/`.

Tag each finding **Important**, **Nit**, or **Pre-existing** (the bug was there before this change). At most 2 Nits. No naming or formatting comments.

Every finding cites `file:line` and a concrete input that fails. No guesses from names.

Start with the number of Important findings. If there are none, say "No issues found."

When the same kind of bug appears in several places, report it once and list every place.

When a finding repeats across reviews, propose a rule for `CLAUDE.md` or a new test instead of repeating it.
