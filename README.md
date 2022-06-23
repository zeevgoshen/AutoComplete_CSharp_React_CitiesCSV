# AutoComplete_React_ASP.NET-CORE

git clone the project running:
"git clone https://github.com/zeevgoshen/AutoComplete_React_ASP.NET-CORE.git"

Running:

Option 1:
Open with Visual Studio 2022, run the solution

Option 2:
in terminal:
cd into SailPoint_AutoComplete_ZG
and run - <br><br><h2><b>"dotnet run"</b></h2><br><br>

in the ouput, search the line saying:

"Now listening on: http://localhost:XXXX" and open this url in the browser.

<hr>

Server summary:

1. In this project I used the Trie data structure since it's the recommended data structure for an AutoComplete feature.
(see comparison with other data structures:)
https://visualstudiomagazine.com/Articles/2015/10/20/Text-Pattern-Search-Trie-Class-NET.aspx?Page=1

2. I used Caching to avoid expansive data fetching (in this case a CSV file, could be DB or others)

3. The Cache Manager is a double-check Singleton to provide thread-safety.
https://riptutorial.com/csharp/example/3864/lazy--thread-safe-singleton--using-double-checked-locking-

4. Trie usage can be optimized, currently it searches letter-by-letter, to use debounce in the client
the sever needs to be changed to accept longer strings.

5. The controller is not using Tasks yet.



Client summary:

1. Cypress is used for testing the client, launch test runner command - "npx cypress open" 

2. De-bounce should be considered as a way to reduce api calls.

3. Other client side caching strategies rather than useState can be considered.

