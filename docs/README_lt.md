# BillioIntegrationTest

[English](../README.md)

Biller API testai nuo pradžios iki galo (end-to-end). Jie kviečia veikiančią API per HTTP, per tuos
pačius tipizuotus klientus, kuriuos naudoja naudotojo sąsaja, ir tikrina kiekvieną atsakymą:
naudotojų, pardavėjų, serijų, pirkėjų, prekių ir sąskaitų, įskaitant klaidas, kurias grąžina
kiekvienas galinis taškas (endpoint).

Šis failas yra [README.md](../README.md) vertimas. Komandos, keliai ir pavadinimai palikti tokie,
kokie yra angliškame faile.

> **Testai per API ištrina visus duomenis prieš paleidimą ir po jo.** Niekada nenukreipkite jų į
> API, kurios duomenis norite išsaugoti.

## Keturios repozitorijos

| Repozitorija | Kas tai |
|---|---|
| [Biller](https://github.com/aldisadom/Biller) | API, duomenų bazės migracijos ir PDF dokumentai |
| [BillerContracts](https://github.com/aldisadom/BillerContracts) | Užklausų ir atsakymų įrašai, išvardijimai (enum) ir tipizuoti HTTP klientai, skelbiami kaip NuGet paketai `BillerContracts` ir `BillerContracts.Clients` |
| [BillerFrontEnd](https://github.com/aldisadom/BillerFrontEnd) | Blazor naudotojo sąsaja. API ji kviečia per `BillerContracts.Clients` |
| [BillioIntegrationTest](https://github.com/aldisadom/BillioIntegrationTest) | Testai, kurie kviečia veikiančią API per HTTP |

## Ko reikia

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Veikianti Biller API su duomenų baze, kurios negaila prarasti
- Toliau aprašytai laikinai testų aplinkai: Docker, Biller darbinė kopija `../Biller` ir bash su `curl`,
  `python3` ir `flock` (Linux arba WSL Windows sistemoje)

## Paleidimas

Saugiausia naudoti laikiną Biller testų aplinką, kuri turi savo duomenų bazę ir prievadus. Iš
`../Biller`:

```sh
bash .claude/skills/_test-stack/scripts/test-stack.sh up
bash .claude/skills/_test-stack/scripts/run-integration.sh
bash .claude/skills/_test-stack/scripts/test-stack.sh down
```

`run-integration.sh` leidžia testus iš aplanko `BillioIntegrationTest`, esančio šalia Biller. Jei
darbinė kopija yra kitame aplanke (worktree), pridėkite `--repo` su jos keliu, santykiniu nuo
aplanko, iš kurio leidžiate komandą, arba absoliučiu, pavyzdžiui, `--repo ../BillioIntegrationTest-BIL-93`
iš `../Biller`, kitaip bus testuojamas ne tas kodas. Norėdami testuoti kitą API, ne tą, kuri
veikia laikinoje testų aplinkoje, pridėkite `--url http://localhost:<port>`: tada scenarijus nustato visus šešis toliau
nurodytus adresus.

Su API, kurią paleidote patys:

```sh
dotnet test BillioIntegrationTest.sln
```

## Konfigūracija

`src/IntegrationTests/appsettings.json` turi API adresus skiltyje `ClientsOptions`, po vieną URL kiekvienam
ištekliui, numatytai `http://localhost:8090/...`. Aplinkos kintamasis pakeičia vieną adresą.
Nustatykite visus šešis: praleistas adresas vis tiek rodo į prievadą 8090, ir testai ištrintų ten
esančius duomenis. WSL aplinkoje su Windows `dotnet.exe` eksportuoti kintamieji jo nepasiekia,
nebent jų pavadinimai yra įrašyti į `WSLENV`: pridėkite `export WSLENV="${WSLENV:+$WSLENV:}ClientsOptions__UserUrl:ClientsOptions__SellerUrl:ClientsOptions__SeriesUrl:ClientsOptions__CustomerUrl:ClientsOptions__ItemUrl:ClientsOptions__InvoiceUrl"`
prie toliau esančio bloko. `run-integration.sh` tai prideda pats, kai rasto dotnet pavadinimas yra
`dotnet.exe`, bet ne tada, kai į jį rodo `dotnet` apvalkalas (wrapper).

```sh
export ClientsOptions__UserUrl=http://localhost:8091/users
export ClientsOptions__SellerUrl=http://localhost:8091/sellers
export ClientsOptions__SeriesUrl=http://localhost:8091/series
export ClientsOptions__CustomerUrl=http://localhost:8091/customers
export ClientsOptions__ItemUrl=http://localhost:8091/items
export ClientsOptions__InvoiceUrl=http://localhost:8091/invoices
dotnet test BillioIntegrationTest.sln
```

Testai naudoja TUnit, kuris veikia su Microsoft.Testing.Platform; `global.json` įjungia šį
vykdytuvą komandai `dotnet test`.

## Šakos ir CI

API pakeitimas ir jo testai daromi tokio paties pavadinimo šakose Biller repozitorijoje ir čia. CI
(`.github/workflows/ci.yml`) nuklonuoja Biller šaką tokiu pačiu pavadinimu kaip ši šaka, o jei
tokios nėra, Biller master, paleidžia ją per Docker Compose ir leidžia su ja testus. Biller CI daro
tą patį atvirkščiai.

## Kur kas yra

- `src/IntegrationTests/Tests/`: po vieną failą kiekvienam ištekliui su jo testais ir testų
  duomenimis, taip pat `TestPreparation.cs`, kuris ištrina visus duomenis prieš paleidimą ir po jo,
  ir `Paging.cs`. Testai vykdomi lygiagrečiai; `[DependsOn]` testą paleidžia po tų, kurių duomenis
  jis naudoja.
- `src/IntegrationTests/Models/`: testų atvejų ir testų duomenų tipai.
- `src/IntegrationTests/ApiClients/`: klientai ir pagalbinė klasė, kuri ištrina vieno ištekliaus
  eilutes.

Kaip sudaryti testai, susitarimai ir peržiūros taisyklės yra [CLAUDE.md](../CLAUDE.md) ir
[REVIEW.md](../REVIEW.md) failuose.

## Licencija

MIT, žr. [LICENSE.txt](../LICENSE.txt).
