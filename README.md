# URL Shortener – Tutorijal za ASP.NET Core Minimal APIs

## Uvod

Ovaj repozitorijum predstavlja **tutorijal i demonstraciju tehnologije ASP.NET Core Minimal APIs**, realizovanu kroz praktičan projekat servisa za skraćivanje URL-ova (URL Shortener).

Cilj tutorijala je da:

- objasni **koji problem Minimal APIs rešavaju**,
- predstavi njihove **ključne karakteristike**,
- i demonstrira njihovu upotrebu kroz **jednostavan, ali realan primer**.

Projekat je razvijen kao deo nastavnih obaveza i namenjen je da zameni klasičan seminarski rad.

---

## Problem koji tehnologija rešava

Klasičan pristup razvoju web API aplikacija u ASP.NET Core okruženju (MVC, Controllers) često:

- zahteva veliku količinu boilerplate koda,
- uvodi nepotrebne apstrakcije,
- otežava brzo prototipiranje manjih servisa.

Za jednostavne servise kao što su:

- REST API-ji,
- mikroservisi,
- interni backend servisi,

potrebno je **lakše i direktnije rešenje** koje omogućava brzo mapiranje HTTP zahteva na logiku aplikacije.

---

## Rešenje: ASP.NET Core Minimal APIs

**Minimal APIs** omogućavaju:

- definisanje ruta direktno u `Program.cs`,
- automatski model binding,
- integraciju sa Dependency Injection mehanizmom,
- jednostavan razvoj REST servisa uz minimalnu konfiguraciju.

Ovaj projekat koristi Minimal APIs kao centralnu backend tehnologiju i demonstrira:

- mapiranje HTTP ruta (`MapGet`, `MapPost`, `MapPut`),
- rad sa zaglavljima (API Key),
- povezivanje sa bazom podataka,
- redirekciju HTTP zahteva.

---

## Arhitektura sistema

Sistem je podeljen na tri osnovne celine:

### Komponente sistema

- **Angular klijentska aplikacija**
  - korisnički interfejs
  - slanje zahteva ka backendu

- **ASP.NET Core Minimal API**
  - obrada zahteva
  - validacija i logika aplikacije
  - redirekcija kratkih URL-ova

- **SQL Server baza**
  - čuvanje URL-ova i statistike klikova
  - pokrenuta u Docker kontejneru

---

## Korišćene tehnologije

### Backend

- ASP.NET Core
- Minimal APIs
- Entity Framework Core
- SQL Server

### Frontend

- Angular
- Angular Material
- RxJS

### Infrastruktura

- Docker
- Docker Compose

---

## Struktura repozitorijuma

/
├── URLShorteningService # Backend – Minimal API
├── urlShortenerClient # Frontend – Angular
├── docker-compose.yml # SQL Server kontejner
├── .gitignore
└── README.md

---

## Pokretanje projekta

### Preduslovi

Neophodno je da su instalirani:

- .NET SDK (7 ili noviji)
- Node.js (LTS)
- Angular CLI
- Docker i Docker Compose

---

### 1️⃣ Pokretanje baze podataka (Docker)

Iz root direktorijuma projekta pokrenuti:

docker compose up -d

Ovim se pokreće SQL Server baza unutar Docker kontejnera.

---

### 2️⃣ Primena migracija baze podataka

U backend projektu izvršiti:

cd URLShorteningService
dotnet ef database update

---

### 3️⃣ Pokretanje backend aplikacije

U backend folderu pokrenuti:

dotnet run

Backend aplikacija će biti dostupna na:

http://localhost:5000

---

### 4️⃣ Pokretanje frontend aplikacije

U frontend folderu izvršiti:

cd urlShortenerClient
npm install
ng serve

Frontend aplikacija će biti dostupna na:

http://localhost:4200

---

## Korišćenje aplikacije

### Korišćenje samo backend servisa

Minimal API omogućava direktno korišćenje kroz browser:

http://localhost:5000/{shortUrl}

Primer:

http://localhost:5000/abc123

Ovim se vrši redirekcija na originalni URL.

---

### Korišćenje kompletne aplikacije

Aplikacija omogućava:

- prijavu korisnika putem email-a
- kreiranje kratkih URL-ova
- pregled postojećih URL-ova
- praćenje broja klikova po URL-u
