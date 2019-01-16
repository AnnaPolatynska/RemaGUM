# RemaGUM - program bazodanowy.

Program został napisany w C# w środowisku Microsoft Visual Studio Enterprise 2017 w oparciu o bazę danych Access. 
Dostęp do bazy danych realizowany jest za pomocą języka zapytań SQL i odbywa się trójwartwowo (plik nsAccess2DB). 
Baza danych zapewnia pełną obsługę CRUD dla przechowywania zdjęć, linków i wszelkich niezbędnych danych. 
Program zawiera plik pomocy bezpośredniej dla użytkownika programu - utworzony przy wykorzystaniu HTMLWorkshop, 
dostępny z poziomu programu.  

1.	Zapotrzebowanie
Oprogramowanie napisanie dla potrzeb Warsztatu Głównego Urzędu Miar. Program ma umożliwić zarządzanie warsztatem 
i magazynem materiałów i normaliów a tym samym ułatwić pracę osobie zarządzającej warsztatem i technikowi.

2.	Cel powstania
Program ewidencyjny maszyn i urządzeń w warsztacie GUM, oraz stanów magazynowych warsztatu. 
Stanowi on punkt wyjścia do stworzenia rozwiązań do zarządzania majątkiem poszczególnych  laboratoriów GUM.
Cel powstania: ułatwienie ewidencjonowania i monitorowania stanu technicznego oraz napraw maszyn w warsztacie, 
oraz bieżące śledzenie stanów magazynowych . 

Program składa się z dwóch modułów:
1. spis maszyn - poprzez bieżącą aktualizację np.: operatora maszyny, osoby zarządzającej, pomieszczenia, 
dodawanie nowego sprzętu i usuwania zużytego oraz monitorowania stanu technicznego i planu napraw i czasu wyłączenia z eksploatacji
poszczególnych maszyn.
2. spis magazynowy - poprzez  aktualizację, bieżącego śledzenia dostępnych ilości poszczególnych produktów, 
ich bieżącego zużycia, ilości odpadów itp., jak również informować o konieczności dokonania zakupów w przypadku przekroczenia stanu
minimalnego. Spisy magazynowe powinny zawierać również stany magazynowe: wszelkich potrzebnych narzędzi 
i drobnych sprzętów warsztatowych jak: wiertła, klucze, frezy, gwintowniki itd. , z możliwością odpisywania jako zużyte, 
w przypadku braku niezbędnej części – generowania informacji o konieczności zakupu.


Pełna dokumentacja znajduje się w katalogu: \RemaGUM \Docs
