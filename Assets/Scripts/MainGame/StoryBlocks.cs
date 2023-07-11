using UnityEngine;
using TMPro;

public class StoryBlocks : MonoBehaviour
{
   
private GameUI gameUI;

private ItemDrop itemDrop;

public StoryBlock currentBlock; // The reference to the built story block
                             // In this format, we can use, among others, the first component of such a block, 
                             // which is its ID and track the progress of our game



// This region describes the structure of the text block and the game, how it is displayed, and how to move between blocks.
 [System.Serializable]
public class StoryBlock {
    // This is visible in the inspector and thanks to that, a developer can see what text is included in each of the blocks.
    [TextArea]
    public string story;
    [Header("This Block ID")]
    public int ThisBlockID;

    [Header("Button 1")]
    public string option1Text;
    public int option1BlockId;
    [Header("Button 2")]
    public string option2Text;
    public int option2BlockId;
     [Header("Button 3")]
    public string option3Text;
    public int option3BlockId;
    [Header("LAST OPTION")]

    public string option4Text;
     
    public StoryBlock(int ThisBlockID, string story, string option1Text = "", string option2Text = "",string option3Text = "", string option4Text = "",
      
        int option1BlockId = -1, int option2BlockId = -1, int option3BlockId = -1) {
        
        this.ThisBlockID = ThisBlockID;
        this.story = story;
        this.option1Text = option1Text;
        this.option2Text = option2Text;
        this.option3Text = option3Text;
        this.option4Text = option4Text;
        this.option1BlockId = option1BlockId;
        this.option2BlockId = option2BlockId;
        this.option3BlockId = option3BlockId;
    }
// The decscription of the method above is really important to understand. 
// It is build out from few values: 1 intiger, 5 strings and 3 intigers.

// The 4 strings are for the text displayed on the buttons, and the 5th one is for the main text of the game
// They are declared as a block together with an assigned ID, because each block have got a different piece of text written on it.
// Eg. ID: 1 -> YourOption1, YourOption2, YourOption3, YourLastDecision, MainGameText
//     ID;2 -> DifferentOption1, DifferentOption2, DifferentOption3, DifferentLastDecision, NowGameStoryIsDifferent

// There are also some intigers. The first contains the ID of the current block (used to identify the block)
// the rest of them is assgined to a different option buttons(they are there to move from block to block)
// Based on that ID, every buttons moves a player to a different ID of the story block. Basically, an intiger is a pointer to different storyblock.

}

   public StoryBlock[] storyBlocksList = {
     // This is the actual list of story blocks
                                        // Storyblock is build like this: Int, String, String, String, String, String, Int, Int, Int
                                        // The first int: ID of the block
                                        // The First string: Main text of the game
                                        // 3 strings: 3 option buttons
                                        // 4th string: previous choosen option
                                        // Three intigers: each one assigned to coresponding option buttos. If a line finishes with numbers eg. 5,6,7 ->
                                        // that means that option1 moves to block nr5, option2 moves to block nr 6, option3 moves to block nr 7.
////////////////////////////////////////////////////////////////////// LAVA LEVEL ///////////////////////////////////////////////////////////////////////////////
   // Block 0
////////////////////////////////////////////////////////////////////// OGNISTA KRAINA ////////////////////////////////////////////////////////////////////// // Block 1 new StoryBlock(1, "Czujesz gorące powietrze, które przepływa przez twoje płuca. Ognista kraina testuje twoją wytrzymałość i determinację.", "Badaj otoczenie", "Idź dalej", "Rozmawiaj z drużyną", "", 2, 3, 4),
// Block 2 
new StoryBlock(2, "Wędrujesz przez żarzące się skały, czując płomienne podmuchy wokół siebie. Jest to niebezpieczne, ale nie możesz się zatrzymać.", "Podążaj naprzód", "Sprawdź, czy jest inna droga", "Porozmawiaj z drużyną", "1", 5, 6, 7),
// Block 3 
new StoryBlock(3, "Czujesz, że jesteś coraz bliżej swojego celu. Ognista twierdza musi być tuż za tymi płonącymi wzgórzami.", "Kontynuuj poszukiwania", "Zbadaj okolicę", "Porozmawiaj z drużyną", "2", 8, 9, 10),
// Block 4 
new StoryBlock(4, "Wędrujesz przez gęste chaszcze, które przechodzą w płomienie na twoim dotarciu. Czy musisz przejść przez ten piekielny szlak?", "Idź naprzód", "Spróbuj znaleźć inną ścieżkę", "Porozmawiaj z drużyną", "3", 11, 12, 13),
// Block 5 
new StoryBlock(5, "Pokonujesz coraz wyższe płonące skały, czując jak temperatura rośnie wraz z każdym krokiem. Czy uda ci się dotrzeć do twierdzy?", "Kontynuuj wędrówkę", "Poszukaj ukrytych przejść", "Porozmawiaj z drużyną", "4", 14, 15, 16),
// Block 6 
new StoryBlock(6, "Przed tobą rozciąga się rozległa dolina ognia, a twierdza jest widoczna w oddali. Jesteś coraz bliżej swojego celu.", "Ruszaj do przodu", "Szukaj skrótu", "Porozmawiaj z drużyną", "5", 17, 18, 19),
// Block 7 
new StoryBlock(7, "Docierasz do bram twierdzy, gdzie płomienie sterczą wokół. Przed tobą otwiera się inny świat - świat tajemnic i niebezpieczeństw.", "Wejdź do twierdzy", "Zbadaj okolicę", "Porozmawiaj z drużyną", "6", 20, 21, 22),

////////////////////////////////////////////////////////////////////// KANAŁY ////////////////////////////////////////////////////////////////////// // Block 8 new StoryBlock(8, "Wchodzisz do mrocznych kanałów, gdzie ciemność otacza cię z każdej strony. Woda jest zimna, a ścieżka wąska.", "Eksploruj kanały", "Wróć na powierzchnię", "Porozmawiaj z drużyną", "", 23, 24, 25),

// Block 9 
new StoryBlock(9, "Kanały są coraz ciaśniejsze i bardziej intrygujące. Czy jesteś gotowy na wyzwania, jakie czekają na ciebie?", "Kontynuuj eksplorację", "Rozważ powrót", "Porozmawiaj z drużyną", "8", 26, 27, 28),
// Block 10 
new StoryBlock(10, "W głąb kanałów docierasz do ukrytych przejść i tajemniczych zakamarków. Czy uda ci się odkryć ich sekrety?", "Podążaj za tajemnicą", "Wróć na powierzchnię", "Porozmawiaj z drużyną", "9", 29, 30, 31),
// Block 11 
new StoryBlock(11, "Mijasz kolejne skrzyżowania w labiryncie kanałów. Czy jesteś na właściwej ścieżce?", "Kontynuuj eksplorację", "Zmień kierunek", "Porozmawiaj z drużyną", "10", 32, 33, 34),
// Block 12 
new StoryBlock(12, "Głosy echa wypełniają przestrzeń w kanałach. Czy to tylko twoja wyobraźnia, czy może ktoś inny jest w pobliżu?", "Badaj dźwięki", "Idź naprzód", "Porozmawiaj z drużyną", "11", 35, 36, 37),
// Block 13 
new StoryBlock(13, "W mrocznych kanałach czujesz się coraz bardziej zagubiony. Czy uda ci się znaleźć wyjście?", "Kontynuuj poszukiwania", "Rozważ powrót", "Porozmawiaj z drużyną", "12", 38, 39, 40),
// Block 14 
new StoryBlock(14, "Podążasz dalej przez kanały, wciąż pełen determinacji. Czy odnajdziesz odpowiedzi na swoje pytania?", "Eksploruj dalej", "Zmień kierunek", "Porozmawiaj z drużyną", "13", 41, 42, 43),
// Block 15 
new StoryBlock(15, "Wąskie tunele kanałów skręcają i rozgałęziają się, tworząc labirynt pełen zagadek.", "Podążaj za instynktem", "Rozważ powrót", "Porozmawiaj z drużyną", "14", 44, 45, 46),
// Block 16 
new StoryBlock(16, "Woda w kanałach jest coraz głębsza, a przepływ coraz silniejszy. Musisz uważać na wiry i pułapki.", "Kontynuuj podróż", "Zastanów się nad powrotem", "Porozmawiaj z drużyną", "15", 47, 48, 49),
// Block 17 
new StoryBlock(17, "W jednym z ciemnych korytarzy zauważasz blask światła. Czy to znak, że zbliżasz się do wyjścia?", "Podążaj za światłem", "Badaj inne korytarze", "Porozmawiaj z drużyną", "16", 50, 51, 52),

////////////////////////////////////////////////////////////////////// BOCZNE WEJŚCIE ////////////////////////////////////////////////////////////////////// // Block 18 new StoryBlock(18, "Odnajdujesz boczne wejście, które prowadzi do górnego poziomu twierdzy. Czy to może być szybsza droga?", "Wejdź przez boczne wejście", "Badaj inne korytarze", "Porozmawiaj z drużyną", "", 53, 54, 55),

// Block 19 
new StoryBlock(19, "Przechodząc przez boczne wejście, trafiasz na mniej strzeżone obszary twierdzy. Czy jest to twoja szansa na uniknięcie pułapek?", "Podążaj dalej", "Zbadaj inne pomieszczenia", "Porozmawiaj z drużyną", "18", 56, 57, 58),
// Block 20 
new StoryBlock(20, "Boczne wejście prowadzi cię przez tajemnicze korytarze. Czy odnajdziesz tam cenne skarby?", "Eksploruj dalej", "Wróć na główną ścieżkę", "Porozmawiaj z drużyną", "19", 59, 60, 61),
// Block 21 
new StoryBlock(21, "Wchodzisz do ukrytej sali przez boczne wejście. To miejsce pełne jest starożytnych artefaktów.", "Badaj artefakty", "Zbadaj inne pomieszczenia", "Porozmawiaj z drużyną", "20", 62, 63, 64),
// Block 22 
new StoryBlock(22, "Przechadzasz się po bocznych korytarzach, czując tajemniczą aurę w powietrzu. Czy odkryjesz jakieś sekrety?", "Kontynuuj eksplorację", "Rozważ powrót", "Porozmawiaj z drużyną", "21", 65, 66, 67),
// Block 23 
new StoryBlock(23, "Boczne wejście prowadzi cię do gabinetu maga. Możesz znaleźć tam wskazówki na temat kryształu.", "Przeszukaj gabinet", "Zbadaj inne pomieszczenia", "Porozmawiaj z drużyną", "22", 68, 69, 70),
// Block 24 
new StoryBlock(24, "W głąb bocznych korytarzy docierasz do zapomnianych lochów, gdzie czai się niebezpieczeństwo.", "Kontynuuj eksplorację", "Rozważ powrót", "Porozmawiaj z drużyną", "23", 71, 72, 73),
// Block 25 
new StoryBlock(25, "Boczne wejście prowadzi cię do komnaty skarbów, gdzie złoto i klejnoty błyszczą w ognistym świetle.", "Badaj skarby", "Zbadaj inne pomieszczenia", "Porozmawiaj z drużyną", "24", 74, 75, 76),
// Block 26 
new StoryBlock(26, "Po długiej i wyczerpującej podróży docierasz do samej Twierdzy. Przed tobą stoi olbrzymia brama, strzeżona przez wartowników.", "Przejdź przez bramę", "Szukaj innego wejścia", "Porozmawiaj z drużyną", "22", 77, 78, 79),
// Block 27 
new StoryBlock(27, "Wkraczasz do wnętrza Twierdzy, gdzie stoisz w majestatycznej Sali Rycerzy. Kryształ jest tuż przed tobą, ale droga do niego nie będzie łatwa.", "Podążaj do Kryształu", "Zbadaj inne pomieszczenia", "Porozmawiaj z drużyną", "26", 80, 81, 82),
// Block 28 
new StoryBlock(28, "W trakcie poszukiwań docierasz do Komnaty Zaklęć, gdzie czarodziej studiuje starożytne manuskrypty.", "Poproś o pomoc czarodzieja", "Szukaj wskazówek samodzielnie", "Porozmawiaj z drużyną", "27", 83, 84, 85),
// Block 29 
new StoryBlock(29, "Podążając dalej przez mroczne korytarze Twierdzy, wchodzisz do Jaskini Potęgi, gdzie czai się straszliwy przeciwnik.", "Staw czoła przeciwnikowi", "Omini przeciwnika", "Porozmawiaj z drużyną", "28", 86, 87, 88),
// Block 30 
new StoryBlock(30, "Nareszcie, po wielu trudach i niebezpieczeństwach, dochodzisz do Sali Kryształu. Promienie światła oślepiają cię, gdy kładziesz rękę na skarbie.", "Zdobądź Kryształ", "Badaj inne pomieszczenia", "Porozmawiaj z drużyną", "29", 89, 90, 91),
// Block 31 
new StoryBlock(31, "Z kryształem w ręku, rozpoczynasz powrót z Twierdzy. Czy uda ci się bezpiecznie opuścić to niebezpieczne miejsce?", "Skradnij się z powrotem", "Walczyć o wyjście", "Porozmawiaj z drużyną", "30", 92, 93, 94),
// Block 32 
new StoryBlock(32, "Opuszczasz Twierdzę i wyruszasz w drogę powrotną do kryjówki w porcie. Cieszysz się ze zdobycia Kryształu, ale wciąż jesteś czujny.", "Kontynuuj podróż", "Zbadaj inne ścieżki", "Porozmawiaj z drużyną", "31", 95, 96, 97),

////////////////////////////////////////////////////////////////////// PORT ////////////////////////////////////////////////////////////////////// 

// Block 33 
new StoryBlock(33, "Po kilku dniach trudnej podróży docierasz do kryjówki w porcie. Jesteś bezpieczny, ale wiesz, że twoje przygody nie skończyły się jeszcze.", "Celebrować zwycięstwo", "Zacząć planować kolejną misję", "Porozmawiaj z drużyną", "32", 98, 99, 100),
// Block 34 
new StoryBlock(34, "Po powrocie do kryjówki w porcie, spotykacie się ze swoją drużyną, by omówić zdobycie Kryształu i zaplanować kolejną misję.", "Przygotuj się do wyprawy", "Zbadaj portowe okolice", "Porozmawiaj z drużyną", "33", 101, 102, 103),
// Block 35 
new StoryBlock(35, "W porcie zauważacie, że ludzie są zainteresowani waszą obecnością. Rozchodzi się wieść o waszych odważnych przygodach i zdobyciu Kryształu.", "Podziękuj za wsparcie", "Zachowaj anonimowość", "Porozmawiaj z drużyną", "34", 104, 105, 106),
// Block 36 
new StoryBlock(36, "Po krótkim przygotowaniu w porcie, wypływacie na morze, gotowi do kolejnej epickiej wyprawy.", "Ruszaj w stronę nieznanego", "Rozważ inne cele", "Porozmawiaj z drużyną", "35", 107, 108, 109),
// Block 37 
new StoryBlock(37, "Podczas żeglugi napotykacie na ogromne morskie potwory. Musisz stanąć do walki, aby obronić statek i swoją drużynę.", "Walczyć z potworami", "Unikaj potworów", "Porozmawiaj z drużyną", "36", 110, 111, 112),
// Block 38 
new StoryBlock(38, "Niestety, morskie potwory są zbyt silne i statek zostaje zatopiony. Z trudem docieracie na bezludną wyspę, gdzie musicie teraz stawić czoła nowym wyzwaniom.", "Zbadaj wyspę", "Poszukaj pomocy", "Porozmawiaj z drużyną", "37", 113, 114, 115),

////////////////////////////////////////////////////////////////////// BEZLUDNA WYSPA //////////////////////////////////////////////////////////////////////
// Block 39 
new StoryBlock(39, "Znajdujecie się na bezludnej wyspie, oddzieleni od reszty świata. Teraz musicie znaleźć sposób na przetrwanie i znaleźć drogę powrotną.", "Zorganizuj obozowisko", "Poszukaj surowców", "Porozmawiaj z drużyną", "38", 116, 117, 118),

// Block 26
new StoryBlock(26, "Po długiej i wyczerpującej podróży docierasz do samej Twierdzy. Przed tobą stoi olbrzymia brama, strzeżona przez wartowników.", "Przejdź przez bramę", "Szukaj innego wejścia", "Porozmawiaj z drużyną", "22", 77, 78, 79),

// Block 27
new StoryBlock(27, "Wkraczasz do wnętrza Twierdzy, gdzie stoisz w majestatycznej Sali Rycerzy. Kryształ jest tuż przed tobą, ale droga do niego nie będzie łatwa.", "Podążaj do Kryształu", "Zbadaj inne pomieszczenia", "Porozmawiaj z drużyną", "26", 80, 81, 82),

// Block 28
new StoryBlock(28, "W trakcie poszukiwań docierasz do Komnaty Zaklęć, gdzie czarodziej studiuje starożytne manuskrypty.", "Poproś o pomoc czarodzieja", "Szukaj wskazówek samodzielnie", "Porozmawiaj z drużyną", "27", 83, 84, 85),

// Block 29
new StoryBlock(29, "Podążając dalej przez mroczne korytarze Twierdzy, wchodzisz do Jaskini Potęgi, gdzie czai się straszliwy przeciwnik.", "Staw czoła przeciwnikowi", "Omini przeciwnika", "Porozmawiaj z drużyną", "28", 86, 87, 88),

// Block 30
new StoryBlock(30, "Nareszcie, po wielu trudach i niebezpieczeństwach, dochodzisz do Sali Kryształu. Promienie światła oślepiają cię, gdy kładziesz rękę na skarbie.", "Zdobądź Kryształ", "Badaj inne pomieszczenia", "Porozmawiaj z drużyną", "29", 89, 90, 91),

// Block 31
new StoryBlock(31, "Z kryształem w ręku, rozpoczynasz powrót z Twierdzy. Czy uda ci się bezpiecznie opuścić to niebezpieczne miejsce?", "Skradnij się z powrotem", "Walczyć o wyjście", "Porozmawiaj z drużyną", "30", 92, 93, 94),

// Block 32
new StoryBlock(32, "Opuszczasz Twierdzę i wyruszasz w drogę powrotną do kryjówki w porcie. Cieszysz się ze zdobycia Kryształu, ale wciąż jesteś czujny.", "Kontynuuj podróż", "Zbadaj inne ścieżki", "Porozmawiaj z drużyną", "31", 95, 96, 97),

// Block 33
new StoryBlock(33, "Po kilku dniach trudnej podróży docierasz do kryjówki w porcie. Jesteś bezpieczny, ale wiesz, że twoje przygody nie skończyły się jeszcze.", "Celebrować zwycięstwo", "Zacząć planować kolejną misję", "Porozmawiaj z drużyną", "32", 98, 99, 100)

   }; 
        public void DisplayBlock(StoryBlock block) {   // This method describes what will be display "inside" my UI objects
                                                        // It takes whatever is currently asigned in storyblock element and connect it to
                                                        // corresponding UI objects, in order to display that for the player
                                                       
         gameUI.mainText.text = block.story;                    
         gameUI.option1.GetComponentInChildren<TMP_Text>().text = block.option1Text;
         gameUI.option2.GetComponentInChildren<TMP_Text>().text = block.option2Text;
         gameUI.option3.GetComponentInChildren<TMP_Text>().text = block.option3Text;
         gameUI.RecentOption.GetComponentInChildren<TMP_Text>().text = block.option4Text; // Display last option text
        currentBlock = block;
       
    }
    // Option Button nr1(left)
    public void DisplayOption1(){
        DisplayBlock(storyBlocksList[currentBlock.option1BlockId]);
        if(itemDrop.RandomNumberChanceToDropAnItem == 2)                     // Each time this method is called, also check if someone
        {                                                           // have their RandomNumber equal to 2, which means that they found an item.
         gameUI.NotificationAboutItem.SetActive(true);                      // If yes, open the notification panel for them
         itemDrop.RandomNumberChanceToDropAnItem= 0;
        }  
        
    }
    // Option Button nr2(middle)
    public void DisplayOption2(){
        DisplayBlock(storyBlocksList[currentBlock.option2BlockId]);
        if(itemDrop.RandomNumberChanceToDropAnItem == 2)
        {
         gameUI.NotificationAboutItem.SetActive(true);
         itemDrop.RandomNumberChanceToDropAnItem= 0;
        } 
    }
    // Option Button nr3(right)
    public void DisplayOption3(){
        DisplayBlock(storyBlocksList[currentBlock.option3BlockId]); 
        if(itemDrop.RandomNumberChanceToDropAnItem == 2)
        {
         gameUI.NotificationAboutItem.SetActive(true);
         itemDrop.RandomNumberChanceToDropAnItem= 0;
        }
    
     }
    
  
}
