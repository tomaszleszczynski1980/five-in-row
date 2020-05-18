# Five in a Row

## Story

Do you remember Tic-tac-toe from the beginning of your programming career?
The big brother is here, Five-in-a-row or [Gomoku](https://en.wikipedia.org/wiki/Gomoku).
Now, this is not a children's game, this is a serious sport with championships
(actually where Eastern European countries shine) and a field of cutting edge AI
research. It is a matter of national pride to create the best machine players
for AI competitions and human training. Your country needs ☛you☚!

## What are you going to learn?

You'll have to:

- break down the game logic into parts and reassemble them into a solid game workflow
- deal with the board as 2D arrays
- write parametrized methods (flexible board size and win condition)
- move around the board programmatically in any directions
- handle edge cases (and literally the edge of the board)
- think about a simple AI algorithm

## Tasks


1. Implement the `Game` constructor to initialize empty `n`-by-`m` board, i.e. a 2D array filled with zeros. The first coordinate is row number, the second is column number

    - The board is stored as a two-dimensional array (indexed by `[row, column]`)
    - Every cell of the board is initialized with `0`
    - The board is accessible via the public `Board` property
    - The rows of the board are independent (changing one row doesn't affect the others)

2. Implement `GetMove()` that asks for user input and returns the coordinates of a valid move on board.

    - The player specifies coordinates as letter and number: `A2` is first row and second column, `C1` is third row and first column, etc.
    - The function returns a tuple of integers (row, column)
    - The returned coordinates start from 0
    - The integers indicate a valid (empty) position on the board
    - If the user provides coordinates that are outside of board, keep asking
    - If the user provides coordinates for a place that is taken, keep asking
    - If the user provides input that doesn't look like coordinates, keep asking

3. Implement `Mark()` that writes the value of `player` (a `1` or `2`) into the `row` & `col` element of the board.

    - If the cell at `row` and `col` is empty, it is marked with `player`
    - It does not do anything if the coordinates are out of bounds
    - It does not do anything if the cell is already marked

4. Implement `HasWon()` that returns `true` if `player` (of value `1` or `2`) has `howMany` marks in a row on the board.

    - Returns `true` if `player` has `howMany` marks in a row on the board.
    - Returns `false` if `player` doesn't have `howMany` marks in a row on the board.

5. Implement `IsFull()` that returns `true` if the board is full.

    - Returns `true` if there are no empty fields on the board
    - Returns `false` otherwise

6. Implement `PrintBoard()` that prints the board to the screen.

    - Players 1 and 2 are indicated with `X` and `O`, and empty fields are indicated with dots (`.`)
    - There are coordinates displayed around the board
    - A 3-by-8 board is displayed in this format:
```
   1  2  3  4  5  6  7  8
A  .  .  .  .  .  .  .  .
B  .  .  .  .  .  .  .  .
C  .  .  .  .  .  .  .  .
```

7. Implement `PrintResult()` that displays the result of the game.

    - If player 1 wins, print "X won!"
    - If player 2 wins, print "O won!"
    - If nobody wins, print "It's a tie!"

8. Use the implemented methods to write a `Play()` method that will run a whole 2-players game. Parameter `howMany` sets the win condition of the game.

    - Player 1 starts the game
    - Players alternate their moves (1, 2, 1, 2...)
    - The game uses `howMany` to set the win condition
    - The board is displayed before each move and also at the end of game
    - The game ends when someone wins or the board is full
    - The game handles bad input (wrong coordinates) without crashing

9. Allow players to quit the game anytime by typing `quit`.

    - When the player types `quit` instead of coordinates, the program exits.

10. Implement player-against-AI mode.

    - The game is fully playable against the computer, AI can play any of the players. This means that when `EnableAi()` is set for a player number then it calls `GetAiMove()` instead of `GetMove()` for that player.
    - Method `GetAiMove()` returns a valid move (if possible) without asking for any input
    - Method `GetAiMove()` returns `null` if board is full
    - Game play is easy to follow as there is a 1 second delay before each AI move implemented in method `Play()`

11. AI is capable of recognizing the easiest opportunities to win the game with one move.

    - Method `GetAiMove()` picks the winning move if there is one on the board

12. AI is capable of recognizing if its enemy could win the game with the next move, and (supposing there is no direct winning move) moves against it.

    - Method `GetAiMove()` (when there is no winning move in one step) picks a move which prevents a certain winning move for its enemy
    - When there is a direct winning move, function `GetAiMove()` still picks that
    - When there are multiple one-step winning options for the enemy, `GetAiMove()` tries to prevent one of them

13. [OPTIONAL] AI tries to look ahead more than one move. Be aware that a serious AI would follow more complicated strategies.

    - If there are no direct winning or loosing options, AI should pick an option that potentionally creates a direct winning option for the next round
    - As an addition to the above, if possible, AI should pick an option that potentionally creates _more than one_ direct winning options for the next round (thus ensuring the victory)
    - When none of the above options are possible, AI picks a cell that is neighbouring an already marked cell


## General requirements


None

## Hints

- Try to come up with a general "pattern matching" feature that checks for
  sequences like `X X X .` or `. X X .` in any directions. Such ability is
  most likely needed for an AI.
- Contrary to the introduction story, creating a really good AI for this game
  is way above the scope of this project. You should focus on the most basic
  aspects of it.
- Furthermore, you don't have to come up with an AI strategy by yourself,
  you can search the internet for strategy descriptions. But, for your own good,
  please don't use external code, implement written instructions instead.

## Starting repository

Follow [this link](https://journey.code.cool/v2/project/team/blueprint/five-in-a-row/csharp) to create your project repository.

## Background materials

- :exclamation: [Arrays](https://learn.code.cool/full-stack/#/../pages/java/arrays)
- [Exceptions in Java](https://www.dummies.com/programming/java/what-you-need-to-know-about-exceptions-in-java/)
