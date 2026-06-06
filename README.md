# EduCatalog CMS 🎓

**EduCatalog CMS** to nowoczesny, lekki system zarządzania treścią (CMS) w architekturze MVP (Minimum Viable Product), dedykowany dla rynku platform e-learningowych, akademii szkoleniowych oraz niezależnych twórców internetowych. Aplikacja pozwala na kompleksowe zarządzanie katalogiem kursów, ich kategoriami oraz bazą prowadzących (trenerów).

Projekt został zbudowany w oparciu o platformę **.NET 8** przy użyciu wzorca **ASP.NET Core MVC** z silnym naciskiem na optymalizację wydajnościową (Backend) oraz zaawansowane wrażenia użytkownika (UX/UI Frontend).

---

## 💼 Wartość Biznesowa projektu

Większość dostępnych na rynku systemów LMS (Learning Management System) jest droga, przeładowana funkcjami i trudna w konfiguracji. **EduCatalog CMS** rozwiązuje ten problem, dostarczając dedykowane narzędzie marketingowo-sprzedażowe:
1. **Zwiększenie Konwersji Sprzedażowej:** Dzięki architekturze *Mobile-First* oraz zaawansowanemu filtrowaniu, potencjalny klient może znaleźć i przejrzeć ofertę szkoleniową w kilka sekund na dowolnym urządzeniu.
2. **Optymalizacja czasu pracy administratora:** Przemyślany panel zarządzania automatyzuje powtarzalne procesy i skraca czas wprowadzania oferty (np. poprzez dynamiczne podglądy danych).
3. **Redukcja kosztów utrzymania infrastruktury:** Wbudowane mechanizmy czyszczenia zasobów serwera zapobiegają marnowaniu przestrzeni dyskowej w chmurze szkoleniowej.

---

## 🚀 Kluczowe Funkcjonalności

### 🔹 Backend i Baza Danych
* **Pełny system CRUD:** Kompletne operacje zapisu, odczytu, edycji i usuwania dla Kursów, Kategorii oraz Trenerów.
* **Architektura Asynchroniczna:** Pełne wykorzystanie `async/await` w komunikacji z bazą danych, co drastycznie zwiększa przepustowość aplikacji.
* **Zaawansowany File Upload:** Bezpieczne przesyłanie plików graficznych (miniaturki kursów, zdjęcia profilowe) wraz z automatycznym generowaniem unikalnych nazw plików (`Guid`), co zapobiega konfliktom w systemie plików.
* **Garbage Collection dla multimediów:** Autorski mechanizm czyszczenia dysku. W momencie usunięcia kursu/trenera lub zmiany grafiki na nową, stary plik jest **fizycznie usuwany** z folderu `wwwroot/images`, eliminując powstawanie tzw. "plików osieroconych".

### 🔹 Frontend i Zaawansowany UX/UI
* **100% Responsywność (RWD):** Projekt zrealizowany w myśl zasady *Mobile-First* przy użyciu frameworka Bootstrap 5. Interfejs płynnie dostosowuje się do ekranów smartfonów, tabletów i komputerów stacjonarnych.
* **Wyszukiwarka z filtrowaniem dynamicznym:** Możliwość jednoczesnego przeszukiwania tekstu i filtrowania katalogu według kategorii szkoleniowych.
* **Wydajna Paginacja (Stronicowanie):** Wyniki na stronie głównej są porcjowane po 9 elementów (układ siatki 3x3) przy użyciu metod LINQ (`.Skip()` i `.Take()`), co optymalizuje czas ładowania i zapobiega przeciążeniu bazy danych. Paginacja została ostylowana w nowoczesnym formacie pigułek (*pills*) z jasną nawigacją tekstową.
* **Dynamiczny podgląd trenera (JavaScript):** Podczas tworzenia lub edycji kursu, wybranie prowadzącego z listy rozwijanej natychmiastowo generuje pod spodem mikro-kartę z jego zdjęciem oraz biogramem. Administrator widzi specjalizację trenera bez konieczności opuszczania formularza.
* **Profesjonalne Ekrany Ostrzegawcze:** Widoki usuwania (`Delete.cshtml`) zostały przeprojektowane na e-komercyjne alerty bezpieczeństwa informujące o integralności bazy danych (ochrona przed usunięciem trenera/kategorii powiązanych z aktywnymi kursami).
* **Personalizowany Favicon SVG:** Domyślna ikona Microsoftu została zastąpiona nowoczesną, skalowalną ikoną wektorową SVG (Data URI) przedstawiającą czapkę akademicką, spójną z marką projektu.

---

## 🛠️ Stos Technologiczny

* **Język programowania:** C# (.NET 8)
* **Framework:** ASP.NET Core MVC
* **ORM (Dostęp do bazy):** Entity Framework Core (Code-First)
* **Baza danych:** Microsoft SQL Server (lub LocalDB)
* **Frontend:** HTML5, CSS3, JavaScript (ES6+), Bootstrap 5, Bootstrap Icons

---

## 🗄️ Struktura Bazy Danych (Encje)

System bazuje na trzech powiązanych ze sobą modelach relacyjnych:
1. **Course (Kurs):** Posiada pola: `Id`, `Title`, `Description`, `Price`, `ImageUrl`, `CategoryId` (Klucz obcy), `TrainerId` (Klucz obcy).
2. **Category (Kategoria):** Posiada pola: `Id`, `Name` oraz relację jeden-do-wielu z kursami.
3. **Trainer (Trener):** Posiada pola: `Id`, `FullName`, `Bio`, `ImageUrl` oraz relację jeden-do-wielu z kursami.

---

## 💻 Instrukcja Uruchomienia

Aby uruchomić projekt lokalnie na swoim komputerze, wykonaj poniższe kroki:

1. **Sklonuj repozytorium:**
   ```bash
   git clone [https://github.com/twoj-username/courses-catalog-cms.git](https://github.com/twoj-username/courses-catalog-cms.git)
   cd courses-catalog-cms
   ```
2. **Przywróć pakiety NuGet:**
   ```bash
   dotnet restore
3. **Wykonaj migrację bazy danych:**
   Upewnij się, że masz poprawnie skonfigurowany Connection
   String w pliku appsettings.json. Następnie w Konsoli Menedżera Pakietów (Package Manager Console) uruchom:
   ```
   Update-Database
   Alternatywnie przez .NET CLI:
4.
   

     
