using System;
using System.Collections.Generic;

class Jumper
{
    // ASCII art representation of the parachute and jumper
     private static readonly List<string> parachute = new List<string>
    {
        "       __________   ", 
        "      ///////  \\\\  ",
        "     ////////   \\\\  ",
        "    //            \\\\ ",
        "   //              \\\\ ",
        "  //                \\\\ ",
        " //__________________\\\\ ",
        "  \\\\               /// ",
        "   \\\\             ///  ",
        "    \\\\           ///  ",
        "     \\\\         ///  ",
        "      \\\\       ///  ",
        "           || ",
        "           || ",
        "           || ",
        "           O  ",
        "          /|\\  ",
        "          / \\  ",
    };

    // List of 50 possible words for the game
    private static readonly string[] words = {
        "Determined", "High", "Nigeria", "Country", "Clandestine",
        "Adventure", "Journey", "Explore", "Courage", "Victory",
        "Challenge", "Wisdom", "Dream", "Aspire", "Legacy",
        "Knowledge", "Bravery", "Friendship", "Passion", "Inspire",
        "Freedom", "Hope", "Strength", "Resilience", "Discovery",
        "Nature", "Harmony", "Unity", "Peace", "Creativity",
        "Justice", "Integrity", "Empower", "Believe", "Focus",
        "Innovate", "Success", "Growth", "Potential", "Ambition",
        "Vision", "Perseverance", "Commitment", "Dedication", "Achievement",
        "Purpose", "Kindness", "Generosity", "Gratitude", "Humility",
        "Serenity", "Authenticity", "Curiosity", "Adventure", "Enlightenment"
    };
    
    // Variables to store the current word, game state, and guesses
    private static string word;
    private static bool isGameOver;
    private static HashSet<char> correctGuesses = new HashSet<char>();
    private static HashSet<char> guessedLetters = new HashSet<char>();

    // Method to select a random word from the list
    private static void GetRandomWord()
    {
        Random random = new Random();
        int randomIndex = random.Next(words.Length);
        word = words[randomIndex].ToLower(); // Convert to lowercase for consistency
    }

    // Method to draw the parachute
    private static void DrawParachute()
    {
        foreach (string line in parachute)
        {
            Console.WriteLine(line); // Print each line of the parachute
        }
        Console.WriteLine(); // Add an extra line for spacing
    }

    // Method to get a letter guess from the user
    private static char GetGuess()
    {
        char guessedLetter;
        do
        {
            Console.Write("Guess a letter in the secret word: ");
            string input = Console.ReadLine().Trim().ToLower();
            if (input.Length == 1 && char.IsLetter(input[0])) // Validate the input
            {
                guessedLetter = input[0];
                break; // Exit loop if valid
            }
            Console.WriteLine("You must enter a single letter."); // Prompt again if invalid
        } while (true);

        return guessedLetter; // Return the guessed letter
    }

    // Method to check if the guessed letter is in the word
    private static void CheckGuess(char guessedLetter)
    {
        if (word.Contains(guessedLetter))
        {
            // Add the letter to correct guesses; if it's already there, don't remove from parachute
            if (!correctGuesses.Add(guessedLetter))
            {
                parachute.RemoveAt(0); // Remove a segment from the parachute if letter already guessed
            }
        }
        else
        {
            parachute.RemoveAt(0); // Remove a segment if the guess is incorrect
        }
    }

    // Method to print the current match of guessed letters
    private static void PrintMatch()
    {
        foreach (char letter in word)
        {
            // Print the letter if guessed correctly, otherwise print an underscore
            Console.Write(correctGuesses.Contains(letter) ? $"{letter} " : "_ ");
        }
        Console.WriteLine(); // Add a new line after printing the word
    }

    // Method to check if the player has won
    private static bool IsWin()
    {
        foreach (char letter in word)
        {
            if (!correctGuesses.Contains(letter)) // If any letter isn't guessed, return false
            {
                return false;
            }
        }
        Console.WriteLine("Congratulations! You won!"); // Notify player of victory
        return true; // Player has won
    }

    // Method to check if the game is over
    private static void CheckGameOver()
    {
        if (parachute.Count == 0) // If there are no segments left, the game is over
        {
            Console.WriteLine("Game Over!");
            Console.WriteLine($"The hidden word is {word}"); // Reveal the word
            isGameOver = true; // Set the game state to over
        }
    }

    // Method to start and run the game
    public void StartGame()
    {
        GetRandomWord(); // Select a random word
        isGameOver = false; // Initialize game state

        // Main game loop
        while (!isGameOver)
        {
            DrawParachute(); // Display the parachute
            PrintMatch(); // Show the current progress in guessing the word
            char guessedLetter = GetGuess(); // Get user input
            guessedLetters.Add(guessedLetter); // Record the guessed letter
            CheckGuess(guessedLetter); // Check if the guess is correct
            if (IsWin()) // Check if the player has won
            {
                break; // Exit the loop if won
            }
            CheckGameOver(); // Check if the game is over
        }
    }
}
