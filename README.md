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

<h2>Please note the first time will take longer than the next runs since npm install is carried out in the background.</h2><br>
<h2>Don't close the terminal/cmd window</h2>
<br>
in the ouput, search the line saying:

"Now listening on: http://localhost:XXXX" and open this url in the browser.

<hr>

Server summary:

1. In this project I used the Trie data structure since it's the recommended data structure for an AutoComplete feature.
(see comparison with other data structures:)
https://visualstudiomagazine.com/Articles/2015/10/20/Text-Pattern-Search-Trie-Class-NET.aspx?Page=1

It is not my Trie implementation and was used 99% "as-is", like a nuget package,
since there is no Trie implementation that is a part of Dot Net.

2. I used Caching to avoid expansive data fetching (in this case a CSV file, could be DB or others)

3. The Cache Manager is a double-check Singleton to provide thread-safety.
https://riptutorial.com/csharp/example/3864/lazy--thread-safe-singleton--using-double-checked-locking-




Client summary:

1. Cypress is used for testing the client, launch test runner command - "npx cypress open" 
	(Home.cy.tsx is a Cypress component testing file. e2e Cypress testing files are outside the ClientApp)

2. De-bounce is used to reduce api calls.

3. Other client side caching strategies can be considered.

***** Most testing was done manually/unit testing and debugging, also with the e2e option of Cypress.


