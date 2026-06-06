# Dokumentacja Techniczna Systemu EduCatalog CMS

## 1. Architektura Rozwiązania i Stos Technologiczny

Aplikacja EduCatalog CMS została zaprojektowana w oparciu o architekturę **MVC (Model-View-Controller)**, która zapewnia wyraźny podział ról w systemie, ułatwiając jego rozwój, skalowanie oraz utrzymanie.

### 1.1. Wykorzystane technologie
* **Platforma:** C# oraz .NET 8.
* **Framework Webowy:** ASP.NET Core MVC.
* **ORM (Mapowanie obiektowo-relacyjne):** Entity Framework Core.
* **Baza Danych:** Microsoft SQL Server (LocalDB w środowisku deweloperskim).
* **Frontend:** HTML5, CSS3, JavaScript (ES6+), Bootstrap 5 oraz Bootstrap Icons.

### 1.2. Wzorzec MVC w praktyce
* **Modele (Models):** Definiują strukturę danych i logikę domenową (encje: `Course`, `Trainer`, `Category`). Zawierają wbudowane atrybuty walidacyjne (np. `[Required]`, `[StringLength]`, `[Range]`), które gwarantują spójność danych i chronią system zarówno na poziomie serwera, jak i walidacji klienckiej.
* **Widoki (Views):** Stanowią warstwę prezentacji dla użytkownika końcowego. Zostały zaimplementowane przy użyciu silnika **Razor** (`.cshtml`), łącząc semantyczny kod HTML z logiką C#. Interfejs został zbudowany zgodnie z podejściem *Mobile-First*, gwarantując pełną responsywność (RWD).
* **Kontrolery (Controllers):** Stanowią mózg operacyjny aplikacji. Odbierają żądania HTTP (GET, POST), inicjują odpowiednie akcje biznesowe (np. zapis plików, filtrowanie danych), komunikują się z bazą danych i zwracają wyrenderowane widoki do przeglądarki użytkownika.

### 1.3. Zaawansowane wzorce architektoniczne
* **Programowanie Asynchroniczne:** Wszystkie operacje wejścia/wyjścia (I/O), takie jak odpytywanie bazy danych czy modyfikacja plików na dysku serwera, zostały zaimplementowane asynchronicznie z użyciem wzorca `async/await` oraz typu `Task<T>`. Zapobiega to blokowaniu wątków serwera i drastycznie zwiększa przepustowość aplikacji pod dużym obciążeniem.
* **Wstrzykiwanie Zależności (Dependency Injection - DI):** Aplikacja wykorzystuje wbudowany kontener DI frameworka ASP.NET Core do bezpiecznego wstrzykiwania kontekstu bazy danych (`ApplicationDbContext`) do kontrolerów. Zapewnia to poprawne zarządzanie cyklem życia połączeń bazodanowych (Scoped).

## 2. Struktura Bazy Danych i Modele

System bazy danych został wygenerowany z wykorzystaniem podejścia **Code-First**. W pierwszej kolejności zaprogramowano modele obiektowe w języku C#, z których mechanizm migracji Entity Framework Core automatycznie wygenerował odpowiednie tabele w relacyjnej bazie danych SQL Server.

Baza opiera się na trzech głównych encjach, które są ze sobą ściśle powiązane kluczami obcymi.

### 2.1. Tabela `Categories` (Kategorie)
Reprezentuje dziedziny/tematykę dostępnych szkoleń.
* `Id` (Klucz główny, INT, Auto-inkrementacja)
* `Name` (NVARCHAR) – Nazwa kategorii.

**Relacje:** Tabela powiązana z tabelą `Courses` relacją **jeden-do-wielu** (Jedna kategoria może posiadać wiele przypisanych kursów).

### 2.2. Tabela `Trainers` (Prowadzący)
Przechowuje dane ekspertów prowadzących szkolenia.
* `Id` (Klucz główny, INT, Auto-inkrementacja)
* `FullName` (NVARCHAR) – Imię i nazwisko prowadzącego.
* `Bio` (NVARCHAR) – Krótki opis doświadczenia i specjalizacji.
* `ImageUrl` (NVARCHAR) – Ścieżka do zdjęcia profilowego trenera na serwerze.

**Relacje:** Tabela powiązana z tabelą `Courses` relacją **jeden-do-wielu** (Jeden trener może być autorem wielu kursów w systemie).

### 2.3. Tabela `Courses` (Kursy)
Główna encja przechowująca szczegóły ofert szkoleniowych.
* `Id` (Klucz główny, INT, Auto-inkrementacja)
* `Title` (NVARCHAR) – Tytuł szkolenia.
* `Description` (NVARCHAR) – Pełny opis kursu.
* `Price` (DECIMAL(18,2)) – Cena szkolenia z obsługą walut.
* `ImageUrl` (NVARCHAR) – Ścieżka do okładki kursu.
* `CategoryId` (Klucz obcy, INT) – Wskazuje na `Categories.Id`.
* `TrainerId` (Klucz obcy, INT) – Wskazuje na `Trainers.Id`.

### 2.4. Integralność referencyjna i Delete Behavior
Baza danych została zaprojektowana z zachowaniem ścisłej integralności. Domyślnie, próba usunięcia `Kategorii` lub `Trenera`, do których przypisane są jakiekolwiek rekordy w tabeli `Courses`, spowoduje zablokowanie operacji (błąd klucza obcego). Ogranicza to ryzyko powstawania tzw. *rekordów sierocych* w bazie danych. Administrator systemu, przed usunięciem trenera, musi najpierw przypisać jego kursy innej osobie lub je usunąć, co jest zabezpieczone po stronie interfejsu (UX) dedykowanymi komunikatami ostrzegawczymi.

## 3. Kluczowe Funkcjonalności i Optymalizacje

Oprócz standardowych operacji CRUD, system został wzbogacony o zaawansowane mechanizmy optymalizujące wydajność serwera oraz poprawiające doświadczenia użytkownika (UX).

### 3.1. Optymalizacja wydajności: Paginacja zapytań (LINQ)
Aby uniknąć problemu N+1 zapytań i przeciążenia pamięci RAM serwera przy dużej liczbie kursów, na stronie głównej wdrożono mechanizm stronicowania. 
Wykorzystując metody rozszerzeń LINQ (`.Skip()` oraz `.Take()`), aplikacja pobiera z bazy danych wyłącznie określoną paczkę danych (np. 9 kursów na stronę). Stan wyszukiwarki (wpisana fraza oraz wybrana kategoria) jest zachowywany w parametrach żądania podczas przełączania stron, co zapewnia płynność nawigacji.

### 3.2. Zarządzanie zasobami: Garbage Collection dla plików
System przesyłania plików został zabezpieczony przed zjawiskiem "zaśmiecania" serwera (akumulacji nieużywanych plików graficznych). W kontrolerach zaimplementowano niestandardową logikę zarządzania dyskiem (`System.IO`).
Gdy administrator edytuje zdjęcie przypisane do kursu/trenera lub całkowicie usuwa rekord z bazy, system najpierw weryfikuje istnienie starego pliku w katalogu `wwwroot/images`, a następnie bezpowrotnie usuwa go fizycznie z dysku przed zapisaniem nowej ścieżki.

### 3.3. Dynamiczny interfejs i JavaScript (UX)
Formularze dodawania i edycji kursów zostały zoptymalizowane pod kątem czasu pracy administratora. Zamiast zmuszać użytkownika do otwierania osobnych zakładek z profilami prowadzących, zastosowano atrybuty `data-*` w języku HTML5. Po wybraniu trenera z listy rozwijanej, dedykowany skrypt JavaScript natychmiastowo buduje i wyświetla animowaną wizytówkę ze zdjęciem i biogramem eksperta, bez przeładowywania strony (redukcja żądań HTTP).

### 3.4. Bezpieczne ekrany usuwania z ostrzeżeniami
Domyślne widoki usuwania (Delete) wygenerowane przez framework zostały całkowicie przeprojektowane. Wprowadzono w nich wyraźne alerty ostrzegawcze (zastosowanie klas `alert-warning` i `text-danger` z biblioteki Bootstrap 5), które jasno informują administratora o konsekwencjach biznesowych i bazodanowych operacji, którą zamierza wykonać.

## 4. Przypadki Użycia (Use Cases)

Aplikacja realizuje założenia biznesowe dla dwóch głównych ról: Użytkownika końcowego (Klienta/Kursanta) oraz Administratora platformy.

### 4.1. Użytkownik końcowy (Gość)
* **Przeglądanie oferty:** Dostęp do przejrzystego katalogu wszystkich aktywnych kursów w systemie.
* **Wyszukiwanie i filtrowanie:** Możliwość błyskawicznego odnalezienia kursu po frazie tekstowej (przeszukiwanie tytułów i opisów) lub zawężenia wyników do konkretnej kategorii tematycznej za pomocą rozwijanej listy.
* **Nawigacja stronicowana:** Wygodne przełączanie się między stronami wyników dzięki zoptymalizowanej paginacji.
* **Wgląd w szczegóły:** Wyświetlanie pełnej karty informacyjnej wybranego szkolenia, zawierającej cenę, pełny opis oraz sylwetkę przypisanego eksperta.

### 4.2. Administrator Systemu
* **Zarządzanie Kursami (CRUD):** Dodawanie nowych ofert szkoleniowych, edycja istniejących parametrów (zmiana ceny, opisu, trenera) oraz bezpieczne usuwanie kursów.
* **Zarządzanie Kadrą Trenerską:** Budowanie centralnej bazy ekspertów, uzupełnianie ich biogramów oraz aktualizacja zdjęć profilowych.
* **Strukturyzacja oferty:** Tworzenie, edytowanie i usuwanie drzewa kategorii, co pozwala na logiczne uporządkowanie rosnącej bazy szkoleń.
* **Zarządzanie multimediami:** Wgrywanie plików graficznych (okładki kursów, zdjęcia profilowe) ze świadomością, że system automatycznie zatroszczy się o optymalizację przestrzeni dyskowej w przypadku ich nadpisania lub usunięcia.