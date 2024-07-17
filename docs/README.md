# Manager Class

## Summary
The `Manager` class is responsible for handling the game's state and managing various aspects of gameplay such as player information, in-game items, gameplay variables, and settings. This class also handles initializing the game and managing game timers.

## Variables

### Intro Setup Variables
- `username`: `public string { get; private set; }`
  - The username of the player.
- `party`: `public List<PartyModel> { get; private set; }`
  - The list of party members.
- `budget`: `public int { get; private set; }`
  - The initial budget for the player.
- `currentMoney`: `public int { get; private set; }`
  - The current amount of money the player has.
- `playerSprite`: `public SpriteModel { get; set; }`
  - The sprite representing the player.
- `partySprites`: `public List<Sprite> { get; private set; }`
  - The list of sprites for the party members.
- `photosTaken`: `public List<Sprite> { get; set; }`
  - The list of photos taken during the game.

### All In-Game Item Variables
- `currentDestination`: `public townLocations { get; private set; }`
  - The current destination in the game.
- `tiresNum`: `public int { get; private set; }`
  - The number of tires the player has.
- `snacksNum`: `public int { get; private set; }`
  - The number of snacks the player has.
- `fishbaitNum`: `public int { get; private set; }`
  - The amount of fish bait the player has.
- `fishCaughtNum`: `public int { get; private set; }`
  - The number of fish caught.
- `encountersNum`: `public int { get; private set; }`
  - The number of encounters experienced.
- `gameNum`: `public int { get; private set; }`
  - The number of games the player has.
- `medicineNum`: `public int { get; private set; }`
  - The number of medicine items the player has.

### Gameplay Variables
- `gasNum`: `public float { get; private set; }`
  - The amount of gas the player has.
- `userHealth`: `public float { get; private set; }`
  - The health of the player.
- `currentProgress`: `public float { get; private set; }`
  - The current progress in the game.

### End Game Scene Variables
- `totalTime`: `public float { get; private set; }`
  - The total time spent in the game.

### Settings
- `audioVolume`: `public float { get; private set; }`
  - The volume of the game's audio.

### Shops Variable
- `itemsInShop`: `public List<ItemModel> { get; set; }`
  - The list of items available in the shop.

## Enums

### townLocations
Defines possible locations in the game:
- `Home`
- `Solvang`
- `Pismo`
- `Monterey`
- `SanFrancisco`

## Methods

### Static Methods
- `public static Manager GetInstance()`
  - Returns the instance of the `Manager` class.

### Unity Lifecycle Methods
- `void Awake()`
  - Ensures a single instance of the `Manager` exists.
- `private void Start()`
  - Initializes the game by calling `InitializeGame()`.

### Initialization Methods
- `void InitializeGame()`
  - Sets up the initial game state, including party members, sprites, budget, and other variables.

### Replay Methods
- `public void ReplayGame()`
  - Restarts the game by destroying the current `Manager` instance and loading the first scene.

### Shop Manager Methods
- `public bool CanAfford(int value)`
  - Checks if the player can afford an item.
- `public int PriceOfItem(string item)`
  - Gets the price of a specified item.

### Time Functions
- `public float GetTotalTimeElapsed()`
  - Returns the total time elapsed since the game started.
- `public void StopTimer()`
  - Stops the game timer.
- `public void StartTimer()`
  - Starts the game timer if it is not already running.

### Party Functions
- `public void AddMember(PartyModel newMember)`
  - Adds a new member to the party.
- `void RemoveMember(string name)`
  - Removes a member from the party by name.
- `public void changeHealthToMember(string name, float healthToAdd)`
  - Changes the health of a specific party member.
- `public void changeHealthToParty(float healthToAdd)`
  - Changes the health of all party members.
- `public void addToParty(string name)`
  - Adds a new member to the party with a specified name.
- `public void removeToParty(string name)`
  - Removes a member from the party by name.
- `public void increaseHealthToMember(string name, float value)`
  - Increases the health of a specific party member.
- `public void decreaseHealthToMember(string name, float value)`
  - Decreases the health of a specific party member.

### Intro Setup Methods
- `public void setUsername(string name)`
  - Sets the username of the player.
- `public void setPlayerSprite(SpriteModel sprite)`
  - Sets the sprite for the player.
- `public void addPartySprite(Sprite sprite)`
  - Adds a sprite to the list of party sprites.
- `public void setBudget(int value)`
  - Sets the budget for the player.
- `public void increaseMoneyCount(int value)`
  - Increases the current money by a specified value.
- `public void decreaseMoneyCount(int value)`
  - Decreases the current money by a specified value.
- `public void setMoneyCount(int value)`
  - Sets the current money to a specified value.

### In-Game Items Methods
- `public void increaseTireCount(int value)`
  - Increases the tire count by a specified value.
- `public void decreaseTireCount(int value)`
  - Decreases the tire count by a specified value.
- `public void setTireCount(int value)`
  - Sets the tire count to a specified value.
- `public void increaseSnackCount(int value)`
  - Increases the snack count by a specified value.
- `public void decreaseSnackCount(int value)`
  - Decreases the snack count by a specified value.
- `public void setSnackCount(int value)`
  - Sets the snack count to a specified value.
- `public void increaseFishbaitCount(int value)`
  - Increases the fish bait count by a specified value.
- `public void decreaseFishbaitCount(int value)`
  - Decreases the fish bait count by a specified value.
- `public void setFishbaitCount(int value)`
  - Sets the fish bait count to a specified value.
- `public void increaseMedicineCount(int value)`
  - Increases the medicine count by a specified value.
- `public void decreaseMedicineCount(int value)`
  - Decreases the medicine count by a specified value.
- `public void setMedicineCount(int value)`
  - Sets the medicine count to a specified value.
- `public void increaseGameCount(int value)`
  - Increases the game count by a specified value.
- `public void decreaseGameCount(int value)`
  - Decreases the game count by a specified value.
- `public void setDestination(townLocations town)`
  - Sets the current destination.

### Gameplay Methods
- `public void setGasCount(float value)`
  - Sets the gas count to a specified value.
- `public void increaseGasCount(float value)`
  - Increases the gas count by a specified value.
- `public void decreaseGasCount(float value)`
  - Decreases the gas count by a specified value.
- `public void increaseUserHealth(float value)`
  - Increases the user's health by a specified value.
- `public void decreaseUserHealth(float value)`
  - Decreases the user's health by a specified value.
- `public void setCurrentProgress(float value)`
  - Sets the current progress.
- `public void increaseRandomEncounter()`
  - Increases the number of random encounters.
- `public void increaseFishCaught()`
  - Increases the number of fish caught.

### Settings Methods
- `public void SetAudioVolume(float value)`
  - Sets the audio volume to a specified value.

### Utility Methods
- `int lessCheck(int initial, int new_value)`
  - Helper method to check and return the result of subtracting `new_value` from `initial`, ensuring the result is not negative.
