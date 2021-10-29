using System;
using System.Linq;
using System.Collections.Generic;

namespace text_adventure
{
    public static class Program
    {
        //print indented text with line break
        static void Print(string text, string space = "\n", int indent = 3, int width = 117)
        {
            List<string> wordsList = text.Split(" ").ToList();
            while (wordsList.Count > 0)
            {
                string currentLine = new(' ', indent);
                while (currentLine.Length <= width && wordsList.Count > 0)
                {
                    currentLine += " " + wordsList.First();
                    wordsList.Remove(wordsList.First());
                }
                Console.WriteLine(currentLine);
            }
            Console.Write(space);
        }

        //computer text
        static void Computer(string text, string space = "", int indent = 20, int width = 100)
        {
            List<string> wordsList = text.Split(" ").ToList();
            while (wordsList.Count > 0)
            {
                string currentLine = new(' ', indent);
                while (currentLine.Length <= width && wordsList.Count > 0)
                {
                    currentLine += " " + wordsList.First();
                    wordsList.Remove(wordsList.First());
                }
                Console.WriteLine(currentLine);
            }
            Console.Write(space);
        }

        //center text
        static void Centered(string text, string space = "", int indent = 45, int width = 100)
        {
            List<string> wordsList = text.Split(" ").ToList();
            while (wordsList.Count > 0)
            {
                string currentLine = new(' ', indent);
                while (currentLine.Length <= width && wordsList.Count > 0)
                {
                    currentLine += " " + wordsList.First();
                    wordsList.Remove(wordsList.First());
                }
                Console.WriteLine(currentLine);
            }
            Console.Write(space);
        }

        //writes prompt line in grey and takes user input
        static string Prompt(string promptLine = "    > ")
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(promptLine);
            string userInput = Console.ReadLine();
            Console.ResetColor();
            Console.Write("\n\n");
            return userInput;
        }

        //put user input into array and iterate through to find instance of expected keyword
        static bool ContainsAny(this string userInput, params string[] keywords)
        {
            foreach (string keyword in keywords)
            {
                if (userInput.Contains(keyword))
                    return true;
            }
            return false;
        }

        //write captains log menu
        static void Log()
        {
            Console.WriteLine("\n");

            Print("=================================================================", "\n", 30);
            Print("[CONNECTION TO MAIN SERVER OFFLINE NEW ENTRIES WILL NOT BE SAVED]", "\n", 30, 100);
            Print("- |Log 0x1|", "", 30, 100);
            Print("- |Log 0x2|", "", 30, 100);
            Print("- |Log 0x3|", "", 30, 100);
            Print("- |Log 0x4|", "\n", 30, 100);
            Print("- |Exit   |", "\n", 30, 100);
            Print("Select entry to view", "\n", 30, 100);
            Print("=================================================================", "\n", 30);
        }

        //write ship computer menu
        static void Menu()
        {
            Centered("-----------------------------------");
            Console.Write("                                              ");
            //Console.BackgroundColor = ConsoleColor.DarkYellow;
            //Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("......BACKUP PROTOCOL ENGAGED......");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Centered("-----------------------------------");
            Centered("[+]Main Power........Offline....[+]");
            Centered("[+]Secondary Power...Online.....[+]");
            Centered("[+]Shields...........Online.....[+]");
            Centered("[+]Damage Status.....No Damage..[+]");
            Centered("[+]Current Heading...No Heading.[+]");
            Centered("[+]Crew Count:.......23/24......[+]");
            Centered("-----------------------------------", "\n");
            Centered("-----------------------------------");
            Centered("[1]Ship Specifications..........[+]");
            Centered("[2]Crew Manifest................[+]");
            Centered("[3]Cargo Manifest...............[+]");
            Centered("[4]Exit.........................[+]");
            Centered("-----------------------------------", "\n\n");
        }

        //draw lift buttons
        static void LiftButtons()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Centered("      ___________________ ");
            Centered("     |.                 .|");
            Centered("     |                   |");
            Centered("     | [1]  BRIDGE       |");
            Centered("     |                   |");
            Centered("     | [2]  ENGINEERING  |");
            Centered("     |                   |");
            Centered("     | [3]  MEDICAL      |");
            Centered("     |                   |");
            Centered("     | [4]  CARGO BAY    |");
            Centered("     |                   |");
            Centered("     | [5]  QUARTERS     |");
            Centered("     |                   |");
            Centered("     |.                 .|");
            Centered("     |___________________| ", "\n\n");
            Console.ResetColor();
        }
        //check for message heard
        static void MessageHeard(bool x)
        {
            if (x == false) { Print("The new message continues to sound."); }
        }

        //explain mission
        static void GiveMission()
        {
            Console.Write("    \"If you learn who you are ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("FIND YOUR QUARTERS AND IDENTIFY YOURSELF.\"\n");
            Print("\"USE THE RELAY TO REPORT WHAT YOU DISCOVER.\"");
            Console.ResetColor();
        }

        //first correct answer message
        static void FirstCorrect()
        {
            Console.ResetColor();
            Print("After a few moments you hear the sound of the lift traveling to and from the bridge. Then you hear the voice of the ship computer. A corresponding message appears on the relay.", "\n\n");
            Console.ForegroundColor = ConsoleColor.DarkRed;
        }
        //already at location
        static void AtLocation()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Print("> You are already at this location.");
            Console.ResetColor();
        }
        static void Main()
        {
            //console title
            Console.Title = "Blank Space";

            //console size
            Console.SetWindowSize(130, 55); // width, height

            bool gameRunning = true;

            //places visited
            bool liftEntered = false; //enter lift for first time
            bool liftUsed = false; //use lift for first time
            bool bridgeVisited = false; ; //bridge
            bool engineVisited = false; //engineering
            bool medicalVisited = false; //medical
            bool cargoVisited = false; //cargo
            bool quartersVisited = false; //quarters

            //intro vars
            bool getUp = false;
            bool soundHeard = false;
            bool bridgeSeen = false;

            // bridge vars
            bool getRelay = false;
            bool missionAccepted = false;

            //medical vars
            bool bandGet = false;

            //cargo vars
            int cargoVisits = 0;
            bool catSeen = false;
            bool kovaDefeated = false;

            //quarters vars
            bool batonSeen = false;
            bool batonGet = false;

            //win condition stuff
            bool newSoundHeard = false; //win condition explained
            bool riggEntered = false;
            bool beyettEntered = false;
            bool zathreEntered = false;
            bool marceauEntered = false;
            bool crestEntered = false;
            int answer = 0; // track correct guesses


            //set location
            string previousLocation = "";
            string location = "intro";

            while (gameRunning)
            {
                switch (location)
                {
                    /*intro*/
                    case "intro":

                        //title screen
                        Console.WriteLine(@"                .             
                                                                                                    +   
                          .                                                          .                                                                                                    
        
                                                  .                                                            .                                                                
 
                .                      ____  __            __      _____        
                                      / __ )/ /___ _____  / /__   / ___/____  ____ _________ 
                                     / __  / / __ `/ __ \/ //_/   \__ \/ __ \/ __ `/ ___/ _ \ 
                                    / /_/ / / /_/ / / / / ,<     ___/ / /_/ / /_/ / /__/  __/ 
                                   /_____/_/\__,_/_/ /_/_/|_|   /____/ .___/\__,_/\___/\___/             .
                     +                                              /_/                      


       .                                                           .                                              .  


");

                        Console.WriteLine("\n\n");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        while (true)
                        {
                            string i = Prompt("    > Type start to begin: ");
                            if (i.ContainsAny("start", "Start", "START")) { break; }
                        }

                        //intro
                        Console.WriteLine("\n");
                        Print("You see only a dim red light.");
                        Print("A hard surface is against your back.");
                        Print("A bright flash brings your surroundings into view. You hear a repeated sound.");

                        while (true)
                        {
                            //create input prompt
                            string i = Prompt("    > What do you do? ");

                            //handle inputs
                            if (getUp == false && i.ContainsAny("look", "view"))
                            {
                                Print("Your surroundings are now lit by yellow light, but still indistinguishable. Looking straight ahead you see nothing. To either side, you see dots of all colours start to flicker on and off. You try turning your head to look around but cannot. You are laying on the floor.");
                                if (soundHeard == false) { Print("The sound continues to repeat."); }
                            }
                            else if (i.ContainsAny("listen", "sound"))
                            {
                                Print("\"Backup protocol initiated. Secondary power engaged.\"");
                                Print("\"Backup protocol initiated. Secondary power engaged.\"");
                                Print("\"Backup protocol initiated. Secondary power engaged.\"");
                                soundHeard = true;

                                if (bridgeSeen != true && getUp == true)
                                {
                                    Console.WriteLine("\n");
                                    Print("You begin to notice movement around you.");
                                }
                            }
                            else if (i.ContainsAny("stand", "stand up", "get up"))
                            {
                                Print("You stand up. The dots swing upward with you. You had been looking at the ceiling. Now you are able to view the space around you.");
                                getUp = true;
                                if (soundHeard == false) { Print("The sound continues to repeat."); }
                            }
                            else if (getUp == true && i.ContainsAny("look", "view", "movement"))
                            {
                                Print("All around you buttons and screens are flashing to life. You are in a small room filled from floor to ceiling with metal control panels. Chairs are tucked close to several narrow consoles. There is a single door labelled LIFT. A window separates the panels of one wall. You see thousands of stars against pure black. You are on the bridge of a starship.");
                                Print("Five other people are getting to their feet alongside you. One is dressed in red, the others in blue jumpsuits with yellow trim. The jumpsuits are identical. The people in them could not look more different. They look around with blank expressions. Some begin to search the pockets and belt of their suits. You do not recognise anything or anyone.");
                                Print("The repeated noise finally stops and you hear a new message take its place.");
                                bridgeSeen = true;
                                break;
                            }
                            //misc inputs
                            else if (i.ContainsAny("nothing", "wait")) { Print("You take a moment to try and collect your thoughts."); }
                            else if (i.ContainsAny("walk", "run", "go", "shout", "speak", "say")) { Print("You are currently too disorientated to do that."); }
                        }
                        location = "bridge";
                        break;

                    /* bridge*/
                    case "bridge":

                        //return to bridge message.
                        if (bridgeVisited == true) { Print("You return to the bridge. The anonymous crew members are busy at different control stations."); }
                        bridgeVisited = true;

                        //set up action checks
                        int person = 0;
                        string action = "";

                        while (true)
                        {
                            string i = Prompt();

                            //check pockets
                            if (getRelay == false && i.ContainsAny("pocket", "pockets"))
                            {
                                Print("As you reach into your pocket a small square device falls from your suit onto the floor.");
                                while (true)
                                {
                                    i = Prompt();
                                    if (i.ContainsAny("pick", "get", "take", "grab"))
                                    {
                                        Print("You pick the the device up. In its centre is a screen which reads:");

                                        //draw relay message
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("\n");
                                        Print("=== Connection to backup server established ===".ToUpper(), indent: 40, space: "\n\n");
                                        Console.ResetColor();

                                        getRelay = true;
                                        MessageHeard(newSoundHeard);
                                        break;
                                    }
                                }
                            }
                            //use device
                            else if (getRelay == true && i.Contains("device"))
                            {
                                Print("You are not sure what the device does yet.");
                            }
                            //look out window
                            else if (i.ContainsAny("window"))
                            {
                                Print("There is an endless number of stars out in the darkness. They are all perfectly still");
                                MessageHeard(newSoundHeard);
                            }
                            //look at people
                            else if (i.ContainsAny("look", "view", "approach", "inspect", "examine") && i.ContainsAny("people", "blue suits", "crew"))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.Write("    > Which person do you look at? 1 - 5\n\n");
                                Console.ResetColor();
                                action = "look";
                            }
                            else if ((i.Contains("look") && i.ContainsAny("1", "red")) || (action == "look" && !i.ContainsAny("talk", "speak") && i.Contains("1")))// look at quandro
                            {
                                person = 1;
                                Print("They stand straight and tall. Two large compound eyes make up the majority of their narrow face. Above each eye a long feathered antenna curves outward. The rest of their features are obscured by short brown fur. Unlike the others they are dressed in a red tunic. It fits their body without a single visible seam. The front is entirely covered with medals which jangle as they look around. Two spotted wings are folded against their back.");
                                MessageHeard(newSoundHeard);
                            }
                            else if ((i.Contains("look") && i.Contains("2")) || (action == "look" && !i.ContainsAny("talk", "speak") && i.Contains("2"))) // look at crest
                            {
                                person = 2;
                                Print("She is short. Narrow shouldered. Her skin is purple. Now blue. Now silver. She is translucent. Light is passing through her arms and face as she moves. The colours glide over the surface of her skin, like oil on water. It is hard to look away. She is bald with a sharp jaw and pointed ears.");
                                MessageHeard(newSoundHeard);
                            }
                            else if ((i.Contains("look") && i.Contains("3")) || (action == "look" && !i.ContainsAny("talk", "speak") && i.Contains("3"))) //look at beyett
                            {
                                person = 3;
                                Print("He is very tall and slender. His hair is dark. The front and back is cut short but the sides drop down almost to his ankles. The sides seem not to move at all. His hair appears to pass directly through his shoulders, as if he were reaching his arms through an energy field. His features are fine, with high cheekbones. He carries the look of one descended from the original Chinese settlers of Mars.");
                                MessageHeard(newSoundHeard);
                            }
                            else if ((i.Contains("look") && i.Contains("4")) || (action == "look" && !i.ContainsAny("talk", "speak") && i.Contains("4"))) //look at marceau
                            {
                                person = 4;
                                Print("She is a very old lady, still slowly getting to her feet. She is bent double and incredibly short. The skin of her hands and face is dark grey and folded with wrinkles. Her hair is perfect white.");
                                MessageHeard(newSoundHeard);
                            }
                            else if ((i.Contains("look") && i.Contains("5")) || (action == "look" && !i.ContainsAny("talk", "speak") && i.Contains("5"))) //look at zathre
                            {
                                person = 5;
                                Print("She is lean. Average height. The jumpsuit she is wearing is stained and has no sleeves. Her right arm is made entirely of metal. It whirs as she zips through her pockets.");
                                MessageHeard(newSoundHeard);
                            }
                            //talk to people
                            else if ((i.Contains("talk") && i.ContainsAny("people", "blue suits")) || i.ContainsAny("talk to people", "question people", "ask people", "speak to people"))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.Write("    > Which person do you talk to? 1 - 5\n\n");
                                Console.ResetColor();
                                action = "talk";
                            }
                            else if ((i.ContainsAny("talk", "speak") && i.ContainsAny("1", "red")) || (action == "talk" && i.Contains("1")) || (i.ContainsAny("talk to them", "person") && person == 1)) //talk to quandro
                            {
                                Print("They produce a clicking sound as they talk but form words in a deep voice. They speak with stern authority.");
                                Print("\"I am the only one in decorated uniform. I assume commmand of this vessel until the situation is understood.\"");
                                Print("There is no argument from the others.");
                                if (getRelay == true && missionAccepted == false)
                                {
                                    Print("Before you have chance to respond they notice the device in your hand.");
                                    Print("\"You have a RELAY COMMUNICATOR. Take that lift and search the ship. We will attempt to make sense of the controls here and run a full diagnostic.\"");
                                    GiveMission();
                                    missionAccepted = true;
                                }
                            }
                            else if ((i.ContainsAny("talk", "speak") && i.Contains("2")) || (action == "talk" && !i.Contains("look") && i.Contains("2")) || (i.ContainsAny("them", "her", "person") && person == 2)) //talk to crest
                            {
                                Print("She is softly spoken.");
                                Print("\"For a moment I thought you looked familiar.\"");
                                Print("she says.");
                                MessageHeard(newSoundHeard);
                            }
                            else if ((i.ContainsAny("talk", "speak") && i.Contains("3")) || (action == "talk" && !i.Contains("look") && i.Contains("3")) || (i.ContainsAny("them", "him") && person == 3)) //talk to beyett
                            {
                                Print("\"Whatever happened here, I don't expect it to be good.\"");
                                Print(" he says slowly.");
                                MessageHeard(newSoundHeard);
                            }
                            else if ((i.ContainsAny("talk", "speak") && i.Contains("4")) || (action == "talk" && !i.Contains("look") && i.Contains("4")) || (i.ContainsAny("them", "her", "person") && person == 4)) //talk to marceau
                            {
                                Print("It takes a momement for the old woman to register you are speaking. She seems too confused to respond.");
                                MessageHeard(newSoundHeard);
                            }
                            else if ((i.ContainsAny("talk", "speak") && i.Contains("5")) || (action == "talk" && !i.Contains("look") && i.Contains("5")) || (i.ContainsAny("them", "her", "person") && person == 5)) //talk to zathre
                            {
                                Print("\"Don't stare at me like that. I don't know what's going on here either.\"");
                                MessageHeard(newSoundHeard);
                            }
                            //argue
                            else if (i.Contains("argue"))
                            {
                                if (missionAccepted == false && getRelay == false)
                                {
                                    Print("Before you have chance to respond they bark out an order.");
                                    Print("\"Take this RELAY COMMUNICATOR. Use that lift and search the ship. We will attempt to make sense of the controls here and run a full diagnostic.\"");
                                    Print("They hold out a small square device.");
                                    while (true)
                                    {
                                        i = Prompt();
                                        if (i.ContainsAny("take", "pick up", "grab"))
                                        {
                                            Print("You accept the device.");
                                            GiveMission();
                                            missionAccepted = true;
                                            getRelay = true;
                                            break;
                                        }
                                    }
                                }
                                else { Print("You can think of no valid reason to argue."); }
                            }
                            //get in lift
                            else if (i.ContainsAny("lift", "door"))
                            {
                                if (getRelay == true && missionAccepted == true)
                                {
                                    break;
                                }
                                else if (getRelay == true && missionAccepted == false)
                                {
                                    Print("As you go toward the lift the person in red stops you.");
                                    Print("\"You have a RELAY COMMUNICATOR. Take that lift and search the ship. We will attempt to make sense of the controls here and run a full diagnostic.\"");
                                    GiveMission();
                                    break;
                                }
                                else
                                {
                                    Print("As you go toward the lift the person in red stops you.");
                                    Print("\"Take this RELAY COMMUNICATOR. Use that lift and search the ship. We will attempt to make sense of the controls here and run a full diagnostic.\"");
                                    Print("They hold out a small square device.");
                                    while (true)
                                    {
                                        i = Prompt();
                                        if (i.ContainsAny("take", "pick up", "grab"))
                                        {
                                            Print("You accept the device.");
                                            GiveMission();
                                            getRelay = true;
                                            missionAccepted = true;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                            //listen to new message
                            else if (i.ContainsAny("listen", "new message", "message"))
                            {
                                newSoundHeard = true;
                                //explain win condition
                                Print("\"Activation of secondary power has been completed in 4.6 seconds\"");
                                Print("\"Ship computer access is restricted\"");
                                Print("\"Limited manual access can be gained from main engineering terminal\"");
                                Print("\"Emergency medical records are available in medical bay\"");
                                Print("\"To restore access to ship computer ALL SENIOR BRIDGE STAFF MUST CORRECTLY IDENTIFY THEMSELVES in their respective quarters\"");
                            }
                            //use relay
                            else if (getRelay == true && i.ContainsAny("relay", "communicator"))
                            {
                                previousLocation = location;
                                location = "relay";
                                break;
                            }
                        }
                        if (location == "relay")  //case relay
                        {
                            break;
                        }
                        else
                        {
                            previousLocation = location; //case lift
                            location = "lift";
                            break;
                        }

                    /*lift*/
                    case "lift":

                        if (liftEntered == false) //enter lift for first time
                        {
                            Print("You step into the lift. The interior consists of reflective metal panels. You are able to see yourself for the first time. To your left is a console of buttons.\n");
                            LiftButtons();
                            liftEntered = true;
                        }
                        else if (liftEntered == true && previousLocation != "lift") //re-enter lift
                        {
                            Print("You step back into the lift.");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Print("> [1]Bridge [2]Engineering [3]Medical [4]Cargo Bay [5]Quarters");
                        }
                        else { Print("You are in the lift."); } //use relay in lift

                        while (true)
                        {
                            string i = Prompt();
                            //look at self
                            if (i.ContainsAny("self", "reflection", "mirror")) { Print("You are male. Short and stocky. Your face is criss-crossed with thin, straight scars. The bridge of your nose is distinctly off centre and your left ear seems not to match up to your right. The top of your head and jawline are close shaved. The onset of wrinkles is visible among the scars. You are dressed in the same blue jumpsuit."); }
                            //use relay
                            else if (i.ContainsAny("relay", "communicator"))
                            {
                                location = "relay";
                                break;
                            }
                            //look at buttons
                            else if (i.ContainsAny("buttons", "console", "control"))
                            {
                                LiftButtons();
                            }
                            //choose location
                            else if (i.ContainsAny("1", "bridge"))
                            {
                                location = "bridge";
                                if (location == previousLocation) { AtLocation(); }
                                else { break; }
                            }
                            else if (i.ContainsAny("2", "engineering"))
                            {
                                location = "engineering";
                                //if (location == previousLocation) { AtLocation(); }
                                //else
                                break;
                            }
                            else if (i.ContainsAny("3", "medical"))
                            {
                                location = "medical";
                                //if (location == previousLocation) { AtLocation(); }
                                //else
                                break;
                            }
                            else if (i.ContainsAny("4", "cargo bay"))
                            {
                                location = "cargo";
                                //if (location == previousLocation) { AtLocation(); }
                                //else
                                break;
                            }
                            else if (i.ContainsAny("5", "quarters"))
                            {
                                location = "quarters";
                                //if (location == previousLocation) { AtLocation(); }
                                //else
                                break;
                            }
                        }
                        //use lift for first time
                        if (liftUsed == false && location != "relay")
                        {
                            Print("The lift creaks and moves downwards. Then it jolts to the side. Then goes down again.");
                            liftUsed = true;
                        }
                        previousLocation = "lift";
                        break;

                    /*engineering*/
                    case "engineering":

                        if (engineVisited == false)
                        {
                            Print("You exit the lift into a cramped room filled with intricate machinery.");
                            engineVisited = true;
                        }
                        else
                        {
                            Print("You are in engineering.");
                        }

                        while (true)
                        {
                            string i = Prompt();
                            ///use computer
                            if (i.ContainsAny("computer", "terminal"))
                            {
                                location = "computer";
                                break;
                            }
                            //look at hammer
                            else if (i.Contains("hammer") && i.ContainsAny("look", "view", "inspect")) { Print("The hammer is rested with the head against the ground and handle pointing upwards. The handle alone is easily the length of your arm. The flat-sided head is engraved with letters A and Z. Between the letters is a yellow gemstone with a tiny insect suspended inside."); }
                            //pick up hammer
                            else if (i.Contains("hammer") && i.ContainsAny("pick", "take", "grab", "lift")) { Print("The hammer is far too heavy for you to move."); }
                            //look at insect
                            else if (i.Contains("look") && i.ContainsAny("gemstone", "stone", "insect", "beetle")) { Print("The insect is a beetle with one long horn."); }
                            //look around
                            else if (i.Contains("look"))
                            {
                                Print("The room is in near total darkness. The only glow of light comes from the orange screen of a computer terminal embedded in the wall. There is barely room to move even a few steps without needing to squeeze past a component of the ship's engine. There are CAUTION! DANGER! signs everywhere. Propped against a column of pipes is a giant hammer.");
                            }
                            //lift
                            else if (i.ContainsAny("leave", "exit", "lift")) { break; }
                            //relay
                            else if (i.ContainsAny("relay", "communicator"))
                            {
                                previousLocation = location;
                                location = "relay";
                                break;
                            }
                        }
                        if (location == "computer" || location == "relay") { break; } //open computer or relay
                        else
                        {
                            previousLocation = location; //get in lift
                            location = "lift";
                            break;
                        }

                    /*med bay*/
                    case "medical":

                        if (medicalVisited == false)
                        {
                            Print("The lift opens directly onto the medical bay. The room is freezing cold and you hear the sound of dripping water.");
                            medicalVisited = true;
                        }
                        else { Print("You are in the medical bay."); }

                        while (true)
                        {
                            string i = Prompt();
                            //look at computer/medical records
                            if (i.ContainsAny("records", "computer"))
                            {
                                Print("You push the papers aside to use the computer.", "\n\n");
                                location = "med records";
                                break;
                            }
                            //look at paperwork
                            else if (i.Contains("paper"))
                            {
                                Print("The documents are prescriptions for medication. Each one is signed by hand in a spidery scrawl. The name looks to be Eleanee or Evelyne. On another sheet it could be Elodie. The surname begins with the letter M but that is as much as you can tell.");
                                Print("As you sift through the papers you uncover another small device.");
                            }
                            //look/get in pod
                            else if (i.ContainsAny("get in", "look in", "inside")) { Print("The inside of the pod is lined with soft material. If someone or something had been inside, they are nowhere to be seen."); }
                            //look at object
                            else if (i.ContainsAny("object", "steam", "cylinder", "pod")) { Print("You see a curved pod large enough to fit a person. Its glass door is open and fogged with condensation. A pool of water has formed around its base. The pod is empty. On the floor nearby is a small green band."); }
                            //pick up device
                            else if (i.Contains("device"))
                            {
                                Print("The screen shows:", "\n\n");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Print("[NEW AUDIO MESSAGE RECEIVED]", indent: 47);
                                Console.ResetColor();
                            }
                            else if (i.ContainsAny("message", "audio"))
                            {
                                Print("The message begins with a deep sigh.");
                                Print("\"Nurse Dallox please make Corvin a new appointment; he avoided his medical again. He was the same on the Uppsala. He may be young, but he will be slowing down like me before he knows. You used to be able to retire on a doctor's salary long before reaching my age yet here\"");
                                Print("The message clips off. It was not a voice you recognise hearing on the bridge.");
                                Print("You place the device back on the desk.");
                            }
                            //look at desk
                            else if (i.Contains("desk")) { Print(" On the desk is a computer almost buried by paperwork."); }
                            //look at beds
                            else if (i.Contains("beds")) { Print("The beds are clearly used to treat patients of the medical bay. They are all currently empty."); }
                            //listen to water
                            else if (i.Contains("water") && i.ContainsAny("listen", "sound", "look for")) { Print("You locate the source of the noise. You see a curved pod large enough to fit a person. Its glass door is open and fogged with condensation. A pool of water has formed around its base. The pod is empty. On the floor nearby is a small green band."); }
                            //look at water
                            else if (i.ContainsAny("look", "inspect") && i.ContainsAny("pool", "water")) { Print("The pool of water is slowly expanding. There is no evidence that anyone has walked through it."); }
                            //look at band
                            else if (i.Contains("band") && i.ContainsAny("look", "inspect")) { Print("The band is only big enough to fit over your wrist. It has pointed metal studs sticking out around it. It is fastened by a tiny clasp. Hanging from the band is a medallion engraved with the word SPIKE."); }
                            //take band
                            else if (i.ContainsAny("take", "keep", "pick up"))
                            {
                                Print("You put the band into one of your pockets.");
                                bandGet = true;
                            }
                            //wear band
                            else if (i.ContainsAny("wear", "put")) { Print("You see no reason to do that."); }
                            //look around
                            else if (i.Contains("look")) { Print("The room is shaped like a crescent moon. Curved around the wall are beds separated by clear partitions. To the other side is a single desk. On the desk is a computer almost buried by paperwork. At one end of the room is a tall cylindrical object shrouded by steam."); }
                            //lift
                            else if (i.ContainsAny("leave", "exit", "lift")) { break; }
                            //relay
                            else if (i.ContainsAny("relay", "communicator"))
                            {
                                previousLocation = location;
                                location = "relay";
                                break;
                            }
                        }
                        if (location == "med records") //open med records
                        {
                            break;
                        }
                        else if (location == "relay") //use relay
                        {
                            break;
                        }
                        else
                        {
                            previousLocation = location; //get in lift
                            location = "lift";
                            break;
                        }

                    /*cargo*/
                    case "cargo":

                        cargoVisits++;

                        if (cargoVisited == false)
                        {
                            Print("You enter the cargo bay. The room is vast with a high ceiling. A gentle humming sound can be heard.");
                            cargoVisited = true;
                        }
                        else if (previousLocation != "encounter") { Print("You return to the cargo bay."); }

                        while (true)
                        {
                            string i = Prompt();
                            //listen hum
                            if (i.ContainsAny("listen", "hum")) { Print("The sound comes from an enormous field of energy which makes up the far wall of the cargo bay. Through it you can see thousands of stars out in the darkness."); }
                            //go to cat
                            else if (catSeen == true && i.Contains("cat") && i.ContainsAny("go", "approach", "talk", "look", "call", "walk"))
                            {
                                Print("The cat climbs unsteadily down from the shelf.");
                                location = "encounter";
                                break;
                            }
                            //look tube
                            else if (i.Contains("look") && i.ContainsAny("tube", "object")) { Print("The object seems to be in a state of partial assembly or dissasembly. The nearest half of the tube is double the width of the back. It has a rounded end, where a panel is hanging open. The narrow part of the tube ends in three triangular fins."); }
                            //look panel
                            else if (i.Contains("panel")) { Print("The panel opens onto a mess of circuitry. There is an hex key tangled amongst the wires."); }
                            //hex key
                            else if (i.Contains("key")) { Print("You see no reason to do that."); }
                            //look boxes
                            else if (i.Contains("look") && i.ContainsAny("shelves", "boxes", "box")) { Print("The boxes are mostly flat and rectangular in shape. They are each stamped with the logo SKYKEA CORP."); }
                            //look around
                            else if (i.Contains("look"))
                            {
                                Print("The room is lined by huge shelves. They are strapped with hundreds of boxes. Beyond the corridor of shelves an energy barrier separates you from the void of space. Laying horizontally in the centre of the room is a metal tube about the size of a standard shuttle craft.");
                                if (catSeen == true && kovaDefeated == false) { Print("The cat is looking at you from atop one of the shelves. It seems to be having a hard time maintaining its balance."); }
                            }
                            //lift
                            else if (i.ContainsAny("leave", "exit", "lift"))
                            {
                                if (cargoVisits == 1)
                                {
                                    Print("As you are about to re-enter the lift you hear movement behind you. Then a sharp hiss.");
                                    location = "encounter";
                                    break;
                                }
                                else if (cargoVisits > 1 && catSeen == false)
                                {
                                    Print("You start toward the lift. You hear the hissing sound again coming from behind some boxes.");
                                    location = "encounter";
                                    break;
                                }
                                else
                                {
                                    previousLocation = location;
                                    location = "lift";
                                    break;
                                }
                            }
                        }
                        break;


                    /*encounter*/
                    case "encounter":

                        while (true)
                        {
                            string i = Prompt();
                            //run/leave
                            if (i.ContainsAny("run", "get in", "lift", "leave", "exit"))
                            {
                                if (catSeen == false) { Print("You hurry away from the sound without looking back."); }
                                else if (catSeen == true) { Print("You take another step towards the lift. The cat suddenly twitches violently then darts away.", "\n\n"); }
                                previousLocation = location;
                                location = "lift";
                                break;
                            }
                            //look at shape
                            else if (catSeen == false && i.ContainsAny("shape", "boxes", "box"))
                            {
                                Print("You see two glowing eyes flash before you. Then a black cat leaps out into your path.");
                                catSeen = true;
                            }
                            //turn around
                            else if (catSeen == false && i.ContainsAny("behind", "turn", "around")) { Print("You turn around to see a dark shape flicker between two boxes."); }
                            //spike
                            else if (catSeen == true && i.ContainsAny("spike", "Spike"))
                            {
                                Print("You call out to the cat by name. It seems to relax slightly then it darts away.");
                                previousLocation = "encounter";
                                location = "cargo";
                                break;
                            }
                            //pet cat
                            else if (catSeen == true && i.ContainsAny("pet", "stroke", "pick", "talk")) { Print("You bend down toward the cat. It arches its back and hisses at you again."); }
                            //look cat
                            else if (catSeen == true && i.Contains("look") && i.Contains("cat")) { Print("The cat stares directly at you. Its tail is raised and its claws are visible. Its fur is black with no markings. It has no collar."); }
                            //collar
                            else if (catSeen == true && i.ContainsAny("collar", "band") && bandGet == true) { Print("The cat won't allow you to get anywhere near it."); }
                            //kopos kova
                            else if (catSeen == true && i.ContainsAny("kopos kova", "Kopos Kova"))
                            {
                                Print("Stepping toward the cat you speak the name of your prisoner. Before your eyes the body of the cat transforms into the wiry frame of Kopos Kova.");
                                Print("Without saying a word he meets your gaze.");
                                Print("A numb sensation begins to spread throughout your body.");
                                //if (batonGet == true)
                                //{
                                    int lastChance = 0;
                                    while (true)
                                    {
                                        i = Prompt();
                                        if (batonGet == true && i.Contains("baton"))
                                        {
                                            Print("With suprising ease you draw the baton from your belt and lunge forward. You press the tip to Kova's chest and squeeze the trigger. The baton flexes under the force and a jolt of electricty crackles along the blade into Kova's body. The fugitive drops unconcious to the floor.");
                                            Print("The lift opens behind you. The person in the red uniform enters the room accompanied by two other crew members you have not seen before.");
                                            Print("\"We saw what happened from the bridge. I had been keeping an eye on you with the ship's camera's. We will take this creature to the medical bay and make sure he is secured. Carry on with your investigation. Good work.\"");
                                            Print("You watch as they haul the limp body of Kopos Kova into the lift. Warmth begins to return to your limbs and you are able to see the cargo bay clearly again.");
                                            previousLocation = "encounter";
                                            location = "cargo";
                                            kovaDefeated = true;
                                            break;
                                        }
                                        else if (lastChance == 0)
                                        {
                                            Print("You do not have the strength to do that.");
                                            lastChance++;
                                        }
                                        else if (lastChance == 1)
                                        {
                                            Print("Your vision begins to fade.");
                                            lastChance++;
                                        }
                                        else
                                        {
                                            Print("Everything turns black.");
                                            location = "game over";
                                            break;
                                        }
                                    }
                               //}
                                //else
                                //{
                                 //   Print("Then everything turns black.");
                                  //  location = "game over";
                                  //  break;
                                //}
                            }
                            if (location == "game over" || kovaDefeated == true) { break; }
                        }
                        break;

                    /*quarters*/
                    case "quarters":

                        location = "quarters";
                        if (quartersVisited == false)
                        {
                            Print("The lift door opens onto a dim corridor. It winds away from you deep into the ship. On the floor are a series of arrows which glow with the words STAFF QUARTERS. You follow the arrows past rooms lined with bunks. Inside, other members of the ship's crew are excitedly talking. The corridor comes to an end in front of several doors.");
                            quartersVisited = true;
                        }
                        else if (previousLocation == "lift" || previousLocation == "quarters") { Print("You are in the staff quarters."); } //not returning from rooms

                        while (true)
                        {
                            string i = Prompt();
                            //look at doors
                            if (i.ContainsAny("doors", "look")) { Print("Facing you is a central door labelled 103. To your right are two doors labelled 104a and 104b. To your left are doors 201a and 201b."); }
                            //room 103 (rigg)
                            else if (i.Contains("103")) { goto case "103"; }
                            //room 104a (beyett)
                            else if (i.Contains("104a")) { goto case "104a"; }
                            //room 104b (crest)
                            else if (i.Contains("104b")) { goto case "104b"; }
                            //room 201a (zathre)
                            else if (i.Contains("201a")) { goto case "201a"; }
                            //room 201b (marceau)
                            else if (i.Contains("201b")) { goto case "201b"; }
                            //go to relay
                            else if (i.ContainsAny("relay", "communicator"))
                            {
                                previousLocation = location;
                                location = "relay";
                                break;
                            }
                            //lift
                            else if (i.Contains("lift"))
                            {
                                previousLocation = location;
                                location = "lift";
                                break;
                            }
                        }
                        break;

                    /*room 103*/
                    case "103":

                        if (previousLocation == "103") { Print("You are in room 103."); }
                        else { Print("Room 103 appears to be the largest. There is a neatly kept bed, desk and bookshelf. There are no windows. On the desk is a computer terminal next to an open book."); }
                        if (batonGet == false) { Print("Above the shelf a baton is mounted to the wall."); }
                        previousLocation = "103";

                        while (true)
                        {
                            string i = Prompt();
                            //bookshelf
                            if (i.ContainsAny("bookshelf", "books")) { Print("The books look incredibly old. Most are wrapped in plastic. They have titles such as 'A History of Sword Combat' and 'The Art of the Sword'."); }
                            //desk book
                            else if (i.Contains("book")) { Print("You lift the open book from the table and turn it to look at the cover. A piece of paper falls from between the pages. The book's title is 'Swordsmen of the 22nd Century' and the author 'Anatoly Chenov'."); }
                            //paper
                            else if (i.Contains("paper"))
                            {
                                Print(indent: 10, text: "to ernie,");
                                Print(indent: 10, width: 100, text: "sorry to hear about the result of the inquiry, i do not believe the fleet's version of events for one second, i hope this book doesn't bring back too many painful memories but my own works are the only things i can afford to part with, there are not too many people looking for paperbacks even on earth these days,");
                                Print(indent: 10, width: 100, text: "remember my friend: \"nothing in life is fair except a witnessed duel\"");
                            }
                            //look at baton
                            else if (i.Contains("baton") && i.ContainsAny("look", "view", "inspect"))
                            {
                                Print("The baton is a thin cylinder with a squared off tip. A curved wrist guard wraps around a trigger on the handle.");
                                batonSeen = true;
                            }
                            //pick up baton
                            else if (i.Contains("baton") && i.ContainsAny("use", "hold", "pick", "get", "grab", "trigger", "take", "keep"))
                            {
                                if (batonSeen == true)
                                {
                                    Print("You take the baton in one hand and pull the trigger. You feel a vibration through the handle and the blade produces a humming sound. You release the trigger and loop the baton through your belt.");
                                    batonGet = true;
                                }
                                else
                                {
                                    Print("The baton is a thin cylinder with a squared off tip. A curved wrist guard wraps around a trigger on the handle. You take the baton in one hand and pull the trigger. You feel a vibration through the handle and the blade produces a humming sound. You release the trigger and loop the baton through your belt.");
                                    batonSeen = true;
                                    batonGet = true;
                                }
                            }
                            //look around
                            else if (i.ContainsAny("look around", "look at room"))
                            {
                                { Print("Room 103 appears to be the largest. There is a neatly kept bed, desk and bookshelf. There are no windows. On the desk is a computer terminal next to an open book."); }
                                if (batonGet == false) { Print("Above the shelf a baton is mounted to the wall."); }
                            }
                            //leave room
                            else if (i.ContainsAny("go back", "leave", "exit"))
                            {
                                Print("You go back into the corridor.");
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Print("> 103. 104a. 104b. 201a. 201b.");
                                Console.ResetColor();
                                goto case "quarters";
                            }
                            //relay
                            else if (i.ContainsAny("relay", "communicator")) { goto case "relay"; }
                            //captains log
                            else if (i.Contains("computer"))
                            {
                                Print("The computer's screen turns on as you approach.");
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Log();

                                while (true)
                                {
                                    string l = Prompt("                     > ");
                                    string line = "========================================================================================\n";
                                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                                    //entry 1
                                    if (l.ContainsAny("1", "001", "Entry 001", "entry 001"))
                                    {
                                        Computer(line);
                                        Computer("The first leg of our journey is a routine delivery to the Sadda cluster. However as the only ship travelling to this system for the next 6 months, we have been legally compelled to transport a prisoner to the facility on Velorum Prime. We are informed he is an Inonian under the name of Kopos Kova. We do not know the nature of his crime but his extradition from the planet is clearly a priority if the duty has been entrusted to us. I know little about the abilites of the Inonians other than the use of their powers is outlawed outside of their native system. Although instances of this happening are rare, there is little anyone besides members of their own race can do to resist them. For this reason Kova has been put into cryostasis and has been placed into medical where his vital signs can be monitored.", "\n");
                                        Computer("0. Back", "\n");
                                        Computer(line);
                                    }
                                    //entry 2
                                    else if (l.ContainsAny("2", "002", "Entry 002", "entry 002"))
                                    {
                                        Computer(line);
                                        Computer("Alongside Kova we have brought aboard Marshall T'la Quandro. For the safety of my crew I have agreed to transport Kova only on the condition that we are accompanied by a top ranking official to oversee the transfer in person. Due to Kova being in stasis, Quandro has assured me that no additonal security personnel will be neccessary. Unfortunately that is the limit of my bargaining power. Quandro seems to be capable enough. So far we have spoken only about the business at hand. My attempt to break the ice by remarking how much their appearence reminded me of the moths I would catch as a boy on earth was not well recieved.", "\n");
                                        Computer("0. Back", "\n");
                                        Computer(line);
                                    }
                                    //entry 3
                                    else if (l.ContainsAny("3", "003", "Entry 003", "entry 003"))
                                    {
                                        Computer(line);
                                        Computer("Our sensors have detected a ship that may be on our tail. It is currently too far off to scan but its course has not deviated from our own by more than a degree for the past three days. It is possibly just another simple cargo vessel but it is rare to see other ships on an old shipping lane like the one we are travelling. I will have Beyett keep a close eye on its maneuvers.", "\n");
                                        Computer("0. Back", "\n");
                                        Computer(line);
                                    }
                                    //entry 4
                                    else if (l.ContainsAny("4", "004", "Entry 004", "entry 004"))
                                    {
                                        Computer(line);
                                        Computer("We have been scanned by a probe sent from the other ship. It intercepted our course before we could detect its presence however we were able to tractor it aboard before it could return to the other vessel. Room has been made to house it in the cargo bay and I have tasked Beyett and Zathre with doing a full inspection. So far it appears to have made no attempt to detect weapons systems but completed a full scan of our energy cells. We do not know what technology this other ship is capable of or if the probe was able to return any information. I will call a meeting of all senior staff to the bridge at 0500 hours.", "\n");
                                        Computer("0. Back", "\n");
                                        Computer(line);
                                    }
                                    //menu
                                    else if (l == "0") { Log(); }
                                    //exit
                                    else if (l.ContainsAny("exit", "Exit"))
                                    {
                                        Console.ResetColor();
                                        Print("The computer's screen turns off again.");
                                        break;
                                    }
                                }
                            }
                        }

                    /*room 104a*/
                    case "104a":

                        if (previousLocation == "104a") { Print("You are in room 104a."); }
                        else { Print("Room 104a is sparse. The single bunk is unmade but otherwise the room is spotless. At the foot of the bed is a pair of boots. A calendar hangs on the wall beside a small mirror."); }
                        previousLocation = "104a";

                        while (true)
                        {
                            string i = Prompt();
                            //look at boots
                            if (i.Contains("boots")) { Print("The boots are similar to the ones you are wearing only larger. The soles have a thin coating of red dust."); }
                            //look inside boots
                            else if (i.ContainsAny("boots", "boot") && i.ContainsAny("inside", "in")) { Print("You see the word CORVIN written on the inside label of the boots."); }
                            //look at calendar
                            else if (i.Contains("calendar")) { Print("The calendar has 23 months arranged around a red circle. Next to each month is a drawing of an animal. You recognise only a few of them."); }
                            //check bunk
                            else if (i.ContainsAny("bed", "bunk")) { Print("There is nothing unusual about the bed."); }
                            //look at self
                            if (i.ContainsAny("self", "reflection", "mirror")) { Print("You are male. Short and stocky. Your face is criss-crossed with thin, straight scars. The bridge of your nose is distinctly off centre and your left ear seems not to match up to your right. The top of your head and jawline are close shaved. The onset of wrinkles is visible among the scars. You are dressed in the same blue jumpsuit."); }
                            //look around
                            else if (i.ContainsAny("look around", "look at room")) { Print("Room 104a is sparse. The single bunk is unmade but otherwise the room is spotless. At the foot of the bed is a pair of boots. A calendar hangs on the wall beside a small mirror."); }
                            //leave room
                            else if (i.ContainsAny("go back", "leave", "exit"))
                            {
                                Print("You go back into the corridor.");
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Print("> 103. 104a. 104b. 201a. 201b.");
                                Console.ResetColor();
                                goto case "quarters";
                            }
                            //relay
                            else if (i.ContainsAny("relay", "communicator")) { goto case "relay"; }
                        }

                    /*room 104b*/
                    case "104b":

                        if (previousLocation == "104b") { Print("You are in room 104b."); }
                        else { Print("Room 104b is tightly packed with boxes. There is very little habitable space except a bunk accessed by a ladder. On the bunk is a device with a round lens. On the floor are several partly unravelled balls of wool."); }
                        previousLocation = "104b";

                        while (true)
                        {
                            string i = Prompt();
                            //check boxes
                            if (i.ContainsAny("box", "boxes")) { Print("The boxes contain clothes and other personal belongings of the room's occupant. The word HELSINKI is printed on some of the clothing."); }
                            //device
                            else if (i.Contains("device")) { Print("The device is round and flat. It has a single button on the top and a lens pointing outward."); }
                            //press button
                            else if (i.Contains("button")) { Print("A beam of light shines from the lens. You follow it to the wall opposite and see the words: WELCOME BACK ELANDRA."); }
                            //wool
                            else if (i.Contains("wool")) { Print("There are three balls of wool. The strands are tangled and frayed."); }
                            //look around
                            else if (i.ContainsAny("look around", "look at room")) { Print("Room 104b is tightly packed with boxes. There is very little habitable space except a bunk accessed by a ladder. On the bunk is a device with a round lens. On the floor are several partly unravelled balls of wool."); }
                            //leave room
                            else if (i.ContainsAny("go back", "leave", "exit"))
                            {
                                Print("You go back into the corridor.");
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Print("> 103. 104a. 104b. 201a. 201b.");
                                Console.ResetColor();
                                goto case "quarters";
                            }
                            //relay
                            else if (i.ContainsAny("relay", "communicator")) { goto case "relay"; }
                        }

                    /*room 201a*/
                    case "201a":

                        if (previousLocation == "201a") { Print("You are in room 201a."); }
                        else { Print("Room 201a has a messy bunk atop a ladder. Underneath is a desk and chair. Clamped to the desk is an empty vice. A metal armed lamp shines through a magnifying glass. Hex keys of various sizes are scattered across the tabletop."); }
                        previousLocation = "201a";

                        while (true)
                        {
                            string i = Prompt();
                            //leave room
                            if (i.ContainsAny("go back", "leave", "exit"))
                            {
                                Print("You go back into the corridor.");
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Print("> 103. 104a. 104b. 201a. 201b.");
                                Console.ResetColor();
                                goto case "quarters";
                            }
                            //relay
                            else if (i.ContainsAny("relay", "communicator")) { goto case "relay"; }
                            //vice
                            else if (i.Contains("vice")) { Print("The vice is empty. It is the kind you might use in a workshop to hold objects still whilst tinkering with them."); }
                            //magnifying glass
                            else if (i.Contains("magnifying glass")) { Print("Enlarged through the magnifying glass you see a minute yellow crystal. It has a mosquito perfectly presevered inside."); }
                            //look around
                            else if (i.ContainsAny("look around", "look at room")) { Print("Room 201a has a messy bunk atop a ladder. Underneath is a desk and chair. Clamped to the desk is an empty vice. A metal armed lamp shines through a magnifying glass. Hex keys of various sizes are scattered across the tabletop."); }
                        }

                    /*room 201b*/
                    case "201b":

                        if (previousLocation == "201b") { Print("You are in room 201b."); }
                        Print("Room 201b is furnished with an arm chair and table. There is a low bed just barely raised off the ground. In the centre of the floor is a striped rug. On the table is a vase of flowers.");
                        previousLocation = "201b";

                        while (true)
                        {
                            string i = Prompt();
                            //flowers
                            if (i.Contains("flowers"))
                            {
                                Print("The flowers are bright purple but the petals appear to be fabric. They smell faintly of lavender.");
                            }
                            //bed
                            else if (i.Contains("bed"))
                            {
                                Print("The bed is firm with two stacked pillows. At the foot of the bed is a thick blanket.");
                            }
                            //look around
                            else if (i.ContainsAny("look around", "look at room")) { Print("Room 201b is furnished with an arm chair and table. There is a low bed just barely raised off the ground. In the centre of the floor is a striped rug. On the table is a vase of flowers."); }
                            //leave room
                            else if (i.ContainsAny("go back", "leave", "exit"))
                            {
                                Print("You go back into the corridor.");
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Print("> 103. 104a. 104b. 201a. 201b.");
                                Console.ResetColor();
                                goto case "quarters";
                            }
                            //relay
                            else if (i.ContainsAny("relay", "communicator")) { goto case "relay"; }
                        }

                    /*medical records*/
                    case "med records":
                        while (true)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Computer("Enter patient's full name to view emergency medical record. Type 'exit' to close.", "\n");
                            string m = Prompt("                    > ");
                            Console.ForegroundColor = ConsoleColor.Green;

                            //Ernest Rigg
                            if (m.ContainsAny("Ernest Rigg", "ernest rigg"))
                            {
                                Computer("[+] Ernest \'Ernie\' Rigg", "\n");
                                Computer("[.] Identifies  : Male");
                                Computer("[.] Age         : 42 Earth Years");
                                Computer("[.] Height      : 170.18");
                                Computer("[.] Weight      : 81.10");
                                Computer("[.] Homeworld   : Earth", "\n");
                                Computer("[+] Significant Medical History:");
                                Computer("   [Record contains too many entries to display all. Showing recent records.]", "\n");
                                Computer("[.] Laceration (multiple) head, upper torso, right hand, right arm");
                                Computer("[.] Puncture wound (multiple) Abdomen");
                                Computer("[.] Ulna fracture right arm");
                                Computer("[.] Sprain right ankle");
                                Computer("[.] Mandible partial dislocation left side");
                                Computer("[.] Left ear complete reconstruction required");
                                Computer("[.] Outcome: Acceptable", "\n\n");
                            }
                            // ernie
                            else if (m.ContainsAny("Ernie", "ernie")) { Computer("Patient's medical records cannot be accessed by nickname."); }
                            //Corvin Beyett
                            else if (m.ContainsAny("Corvin Beyett", "corvin beyett"))
                            {
                                Computer("[+] Corvin Beyett", "\n");
                                Computer("[.] Identifies  : Male");
                                Computer("[.] Age         : 28 Earth Years");
                                Computer("[.] Height      : 195.58");
                                Computer("[.] Weight      : 60.05");
                                Computer("[.] Homeworld   : Mars", "\n");
                                Computer("[+] Significant Medical History:");
                                Computer("[.] No entries.", "\n\n");
                            }
                            //Elandra Crest
                            else if (m.ContainsAny("Elandra Crest", "elandra crest"))
                            {
                                Computer("[+] Elandra Crest", "\n");
                                Computer("[.] Identifies  : Female");
                                Computer("[.] Age         : 864 Earth Years");
                                Computer("[.] Height      : 165.09");
                                Computer("[.] Weight      : 55.32");
                                Computer("[.] Homeworld   : Rayon V", "\n");
                                Computer("[+] Significant Medical History:", "\n");
                                Computer("[.] Severe anxiety");
                                Computer("[.] Severe depression");
                                Computer("[.] Accute agoraphobia");
                                Computer("[.] Outcome: Treatment ongoing", "\n");
                            }
                            //Amber Zathre
                            else if (m.ContainsAny("Amber Zathre", "amber zathre"))
                            {
                                Computer("[+] Amber Zathre", "\n");
                                Computer("[.] Identifies  : Female");
                                Computer("[.] Age         : 31 Earth Years");
                                Computer("[.] Height      : 167.64");
                                Computer("[.] Weight      : 68.75");
                                Computer("[.] Homeworld   : Earth", "\n");
                                Computer("[+] Significant Medical History:", "\n");
                                Computer("[.] Partial exposure to zero oxygen environment");
                                Computer("[.] Severe hypothermia");
                                Computer("[.] Severe frostbite right arm");
                                Computer("[.] Amputation right arm");
                                Computer("[.] Cybersthetic arm attachment right shoulder");
                                Computer("[.] Fine motor rehabilitation");
                                Computer("[.] Outcome: Full recovery", "\n\n");
                            }
                            //Elodie Marceau
                            else if (m.ContainsAny("Elodie Marceau", "elodie marceau"))
                            {
                                Computer("[+] Elodie Marceau", "\n");
                                Computer("[.] Identifies  : Female");
                                Computer("[.] Age         : 91 Earth Years");
                                Computer("[.] Height      : 144.78");
                                Computer("[.] Weight      : 49.80");
                                Computer("[.] Homeworld   : Ganymede", "\n");
                                Computer("[+] Significant Medical History:", "\n");
                                Computer("[.] Partial loss of hearing");
                                Computer("[.] Double hip replacement");
                                Computer("[.] Outcome: Success", "\n\n");
                            }
                            //Marshall Quandro
                            else if (m.ContainsAny("T'la Quandro", "t'la quandro"))
                            {
                                Computer("[+] T'la Quandro", "\n");
                                Computer("[.] Identifies  : n/a");
                                Computer("[.] Age         : 88 Earth Years");
                                Computer("[.] Height      : 187.62");
                                Computer("[.] Weight      : 81.53");
                                Computer("[.] Homeworld   : Nocturna b", "\n");
                                Computer("[+] Significant Medical History:", "\n");
                                Computer("Medical records not supported. Contact central medical facility on Nocturna b or consult Studies of Arthropedian Anatomy - 11th Edition.", "\n\n");
                            }
                            //Kopos Kova
                            else if (m.ContainsAny("Kopos Kova", "kopos kova"))
                            {
                                Computer("[+] Kopos Kova", "\n");
                                Computer("[.] Identifies  : Male");
                                Computer("[.] Age         : Unknown");
                                Computer("[.] Height      : 177.80");
                                Computer("[.] Weight      : 64.98");
                                Computer("[.] Homeworld   : Inon II", "\n");
                                Computer("[+] Significant Medical History:", "\n");
                                Computer("    Medical records not available", "\n\n");
                            }
                            //Spike
                            else if (m.ContainsAny("spike", "Spike"))
                            {
                                Computer("[+] Spike", "\n");
                                Computer("[.] Identifies  : Male");
                                Computer("[.] Age         : 4 Earth Years");
                                Computer("[.] Height      : 24.02");
                                Computer("[.] Weight      : 3.90");
                                Computer("[.] Homeworld   : Earth", "\n");
                                Computer("[+] Significant Medical History:", "\n");
                                Computer("[.] Subatomic fleas");
                                Computer("[.] Outcome: Regular treatment required.", "\n\n");
                            }
                            //Exit
                            else if (m.ContainsAny("exit", "Exit"))
                            {
                                Console.ResetColor();
                                break;
                            }
                            //wrong name entered
                            else { Computer("Name not recognised.", "\n"); }
                        }
                        location = "medical";
                        break;

                    /*relay*/
                    case "relay":
                        Console.ForegroundColor = ConsoleColor.DarkRed;

                        Print(indent: 40, text: " === CONNECTION TO BRIDGE ESTABLISHED === ");
                        Print(indent: 40, text: "         [ Type \'exit\' to close ]", space: "\n\n\n");

                        while (true)
                        {
                            string c = Prompt("    > Enter senior crew member information: NAME ROLE QUARTERS\n\n      ");
                            Console.ForegroundColor = ConsoleColor.DarkRed;

                            if (riggEntered == false && c.ContainsAny("Ernest Rigg Captain 103", "ernest rigg captain 103", "ERNEST RIGG CAPTAIN 103"))
                            {
                                answer++;
                                Print(indent: 37, text: $"[ Senior crew member identification accepted ({answer}/5) ]");
                                Print(indent: 40, text: "         [ Type \'exit\' to close ]", space: "\n\n\n");
                                riggEntered = true;
                                if (answer == 5) { break; }
                                //Computer(indent: 30, text: "[ The captain must identify themselves in the captain's quarters ]", space: "\n\n\n"); //rigg entered
                            }
                            else if (beyettEntered == false && c.ContainsAny("Corvin Beyett First Mate 104a", "corvin beyett first mate 104a", "CORVIN BEYETT FIRST MATE 104A")) //beyett accepted
                            {
                                answer++;
                                if (answer == 1) { FirstCorrect(); }
                                Print(indent: 37, text: $"[ Senior crew member identification accepted ({answer}/5) ]");
                                Print(indent: 40, text: "         [ Type \'exit\' to close ]", space: "\n\n\n");
                                beyettEntered = true;
                                if (answer == 5) { break; }
                            }
                            else if (marceauEntered == false && c.ContainsAny("Elodie Marceau Doctor 201b", "elodie marceau doctor 201b", "ELODIE MARCEAU DOCTOR 201B")) //marceau accepted
                            {
                                answer++;
                                if (answer == 1) { FirstCorrect(); }
                                Print(indent: 37, text: $"[ Senior crew member identification accepted ({answer}/5) ]");
                                Print(indent: 40, text: "         [ Type \'exit\' to close ]", space: "\n\n\n");
                                marceauEntered = true;
                                if (answer == 5) { break; }
                            }
                            else if (zathreEntered == false && c.ContainsAny("Amber Zathre Chief Engineer 201a", "amber zathre chief engineer 201a", "AMBER ZATHRE CHIEF ENGINEER 201A")) //zathre accepted
                            {
                                answer++;
                                if (answer == 1) { FirstCorrect(); }
                                Print(indent: 37, text: $"[ Senior crew member identification accepted ({answer}/5) ]");
                                Print(indent: 40, text: "         [ Type \'exit\' to close ]", space: "\n\n\n");
                                zathreEntered = true;
                                if (answer == 5) { break; }
                            }
                            else if (crestEntered == false && c.ContainsAny("Elandra Crest Transport Officer 104b", "elandra crest transport officer 104b", "ELANDRA CREST TRANSPORT OFFICER 104B")) //crest accepted 
                            {
                                answer++;
                                if (answer == 1) { FirstCorrect(); }
                                Print(indent: 37, text: $"[ Senior crew member identification accepted ({answer}/5) ]");
                                Print(indent: 40, text: "         [ Type \'exit\' to close ]", space: "\n\n\n");
                                crestEntered = true;
                                if (answer == 5) { break; }

                            }
                            else if (c.ContainsAny("Exit", "exit")) //exit
                            {
                                Console.ResetColor();
                                Print("You put the relay device back into your pocket.");
                                break;
                            }
                            else
                            {
                                Print(indent: 32, text: "[ Crew member identity not recognised or already accepted ]");  //wrong entry
                                Print(indent: 40, text: "         [ Type \'exit\' to close ]", space: "\n\n\n");
                            }
                        }
                        if (answer == 5) //trigger win state
                        {
                            location = "win";
                            break;
                        }
                        else
                        {
                            location = previousLocation; //go to previous location
                            break;
                        }

                    /*computer*/
                    case "computer":
                        Console.ForegroundColor = ConsoleColor.DarkYellow; //change text colour to yellow
                        string page = "default";
                        Menu();

                        while (true)
                        {
                            if (page == "menu") { Menu(); }

                            //create input prompt and set menu text to yellow
                            string p = Prompt("                                             > "); //get input
                            Console.ForegroundColor = ConsoleColor.DarkYellow;

                            if (page.ContainsAny("default", "menu") && p == "1") //main menu 1. ship specs
                            {
                                Centered("-----------------------------------");
                                Centered("........Ship Specifications........");
                                Centered("-----------------------------------");
                                Centered("[+]SCS Oslo.....................[+]");
                                Centered("[+]Loki Class Cargo Freighter...[+]");
                                Centered("[+]Commissioned 2291.17.9 ET....[+]");
                                Centered("[+].............................[+]");
                                Centered("[+]Operated by..................[+]");
                                Centered("[+]SKYKEA Corporation...........[+]");
                                Centered("-----------------------------------", "\n");
                                Centered("-----------------------------------");
                                Centered("[0]Back.........................[+]");
                                Centered("-----------------------------------", "\n");
                                page = "ship specs";
                            }
                            else if ((page.ContainsAny("default", "menu") && p == "2") || (page.ContainsAny("senior", "main", "personnel") && p == "0"))//main menu 2. crew manifest
                            {
                                Centered("   ----------------------------");
                                Centered("   .......Crew Manifest........");
                                Centered("   ----------------------------");
                                Centered("   [1]Senior Bridge Crew....[+]");
                                Centered("   [2]Main Crew.............[+]");
                                Centered("   [3]Additional Personnel..[+]");
                                Centered("   ----------------------------", "\n");
                                Centered("   ----------------------------");
                                Centered("   [0]Back..................[+]");
                                Centered("   ----------------------------", "\n");
                                p = "-1";
                                page = "crew manifest";
                            }
                            else if ((page == "crew manifest" && p == "1") || (page.ContainsAny("record") && p == "0"))//crew manifest 1. senior crew
                            {
                                Centered("------------------------------------");
                                Centered(".........Senior Bridge Crew.........");
                                Centered("------------------------------------");
                                Centered("[1]E. Rigg......Captain..........[+]");
                                Centered("[2]C. Beyett....First Mate.......[+]");
                                Centered("[3]E. Marceau...Doctor...........[+]");
                                Centered("[4]A. Zathre....Chief Engineer...[+]");
                                Centered("[5]E. Crest.....Transport Officer[+]");
                                Centered("------------------------------------", "\n");
                                Centered("------------------------------------");
                                Centered("[.]Select to view service record.[.]");
                                Centered("[0]Back..........................[+]");
                                Centered("------------------------------------", "\n");
                                page = "senior";
                            }
                            else if (page == "senior" && p == "1") //senior crew 1. rigg record
                            {
                                Centered("--------------------------------------------------------", indent: 35);
                                Centered(".................E. Rigg Service Record.................", indent: 35);
                                Centered("--------------------------------------------------------", indent: 35);
                                Centered("[+]SCS Malmö Hel Class Destroyer..........Bosun......[+]", indent: 35);
                                Centered("[+]SCS Copenhagen Odin Class Battleship...Second Mate[+]", indent: 35);
                                Centered("[+][REASSIGNED]......................................[+]", indent: 35);
                                Centered("[+]SCS Oslo Loki Class Cargo Freighter....Captain....[+]", "", indent: 35);
                                Centered("--------------------------------------------------------", "\n", indent: 35);
                                Centered("--------------------------------------------------------", indent: 35);
                                Centered("[0]Back..............................................[+]", indent: 35);
                                Centered("--------------------------------------------------------", "\n", indent: 35);
                                page = "rigg record";
                            }
                            else if (page == "senior" && p == "2") //senior crew 2. beyett record
                            {
                                Centered("------------------------------------------------------", indent: 38);
                                Centered(".............C. Beyett Service Record.................", indent: 38);
                                Centered("------------------------------------------------------", indent: 38);
                                Centered("[+]SCS Uppsala Thor Class Frigate........Ensign....[+]", indent: 38);
                                Centered("[+]SCS Reykjavic Thor Class Frigate......Bosun.....[+]", "", indent: 38);
                                Centered("[+]SCS Oslo Loki Class Cargo Freighter...First Mate[+]", "", indent: 38);
                                Centered("------------------------------------------------------", "\n", indent: 38);
                                Centered("------------------------------------------------------", indent: 38);
                                Centered("[0]Back............................................[+]", indent: 38);
                                Centered("------------------------------------------------------", "\n", indent: 38);
                                page = "beyett record";
                            }
                            else if (page == "senior" && p == "3") //senior crew 3. merceau record
                            {
                                Centered("----------------------------------------------------------", indent: 35);
                                Centered("................E. Marceau Service Record.................", indent: 35);
                                Centered("----------------------------------------------------------", indent: 35);
                                Centered("[+]SCS Uppsala Thor Class Frigate.........Ship Doctor..[+]", indent: 35);
                                Centered("[+]SCS Oslo Loki Class Cargo Freighter....Ship Doctor..[+]", "", indent: 35);
                                Centered("----------------------------------------------------------", "\n", indent: 35);
                                Centered("----------------------------------------------------------", indent: 35);
                                Centered("[0]Back................................................[+]", indent: 35);
                                Centered("----------------------------------------------------------", "\n", indent: 35);
                                page = "merceau record";
                            }
                            else if (page == "senior" && p == "4") //senior crew 4. zathre record
                            {
                                Centered("-------------------------------------------------------------", indent: 35);
                                Centered("...................A. Zathre Service Record..................", indent: 35);
                                Centered("-------------------------------------------------------------", indent: 35);
                                Centered("[+]SCS Oslo Loki Class Cargo Freighter...Junior Engineer..[+]", indent: 35);
                                Centered("[+]......................................Chief Engineer...[+]", "", indent: 35);
                                Centered("-------------------------------------------------------------", "\n", indent: 35);
                                Centered("-------------------------------------------------------------", indent: 35);
                                Centered("[0]Back...................................................[+]", indent: 35);
                                Centered("-------------------------------------------------------------", "\n", indent: 35);
                                page = "zathre record";
                            }
                            else if (page == "senior" && p == "5") //senior cre 5. crest record
                            {
                                Centered("-----------------------------------------------------------------", indent: 33);
                                Centered(".....................E. Crest Service Record.....................", indent: 33);
                                Centered("-----------------------------------------------------------------", indent: 33);
                                Centered("[+]SCS Oslo Loki Class Cargo Freighter.......Ensign...........[+]", indent: 33);
                                Centered("[+]SCS Helsinki Loki Class Cargo Freighter...Ensign...........[+]", indent: 33);
                                Centered("[+]SCS Oslo Loki Class Cargo Freighter.......Transport Officer[+]", "", indent: 33);
                                Centered("-----------------------------------------------------------------", "\n", indent: 33);
                                Centered("-----------------------------------------------------------------", indent: 33);
                                Centered("[0]Back.......................................................[+]", indent: 33);
                                Centered("-----------------------------------------------------------------", "\n", indent: 33);
                                page = "crest record";
                            }
                            else if (page == "crew manifest" && p == "2") //crew manifest 2. main crew
                            {
                                Centered("-----------------------------------");
                                Centered(".............Main Crew.............");
                                Centered("-----------------------------------");
                                Centered("[+]H. Tabqvist......E. Xenothias[+]");
                                Centered("[+]L. Fyth..........K. Eddfield.[+]");
                                Centered("[+]R. Dallox........X. Mbarwal..[+]");
                                Centered("[+]N. Mäkinen.......F. Voltari..[+]");
                                Centered("[+]G. Tabsten.......O. Zhu......[+]");
                                Centered("[+]K. Solenti.......P. Brooklov.[+]");
                                Centered("[+]S. Xeban.........N. Ekkov....[+]");
                                Centered("[+]D. Rivriel.......I. Xuaz.....[+]");
                                Centered("-----------------------------------", "\n");
                                Centered("-----------------------------------");
                                Centered("[0]Back.........................[+]");
                                Centered("-----------------------------------", "\n");
                                page = "main";
                            }
                            else if (page == "crew manifest" && p == "3") // crew manifest 3. personnel
                            {
                                Centered("------------------------------------");
                                Centered("........Additional Personnel........");
                                Centered("------------------------------------");
                                Centered("[+]K. Kova......Prisoner.........[+]");
                                Centered("[+]T. Quandro...Prisoner Escort..[+]");
                                Centered("[+]Spike........Ships Cat........[+]");
                                Centered("------------------------------------", "\n");
                                Centered("------------------------------------");
                                Centered("[0]Back..........................[+]");
                                Centered("------------------------------------", "\n");
                                page = "personnel";
                            }
                            else if (page.ContainsAny("default", "menu") && p == "3") //main menu 3. cargo manifest 
                            {
                                Centered("------------------------------------------", indent: 42);
                                Centered("..............Cargo Manifest..............", indent: 42);
                                Centered("------------------------------------------", indent: 42);
                                Centered("[+]200 x FRIHETEN......200 x VIKTIGT...[+]", indent: 42);
                                Centered("[+]180 x FJÄDERMOLN....180 x FRÖJERED..[+]", indent: 42);
                                Centered("[+]180 x YPPERLIG......180 x STENLILLE.[+]", indent: 42);
                                Centered("[+]140 x BJÖRKSNÄS.....160 x BEKVÄM....[+]", indent: 42);
                                Centered("[+]108 x ÖRFJÄLL.......140 x EKOLSUND..[+]", indent: 42);
                                Centered("[+]100 x EKET..........100 x SVÄRTA....[+]", indent: 42);
                                Centered("------------------------------------------", "\n", indent: 42);
                                Centered("------------------------------------------", indent: 42);
                                Centered("[0]Back................................[+]", indent: 42);
                                Centered("------------------------------------------", "\n", indent: 42);
                                page = "cargo manifest";
                            }
                            else if (p.ContainsAny("4", "exit", "Exit")) // main menu 4. exit
                            {
                                break;
                            }
                            else if (page.ContainsAny("ship specs", "crew manifest", "cargo manifest") && p == "0") //back to menu
                            {
                                page = "menu";
                            }
                            else if (p.Contains("lift"))
                            {
                                Console.ResetColor();
                                previousLocation = "engineering";
                                goto case "lift";
                            }
                        }
                        Console.ResetColor();
                        location = "engineering";
                        break;

                    /*epilogue*/
                    case "win":

                        Console.ResetColor();
                        Print("The voice of the ship computer fills the room.");
                        Print("\"Identification complete\"");
                        Print("\"All senior staff are accounted for\"");
                        Print("\"Full access to computer restored\"");
                        Print("\"Re-engaging main power\"");
                        Print("The lights around you grow brighter and a rumble passes through the deck beneath your feet. You make your way back to the bridge.");

                        Print("Three of your five crew members are waiting for you. Beyett is staring intently at a control panel. Marceau is seated to the left of the captain's chair.");

                        Print("Red uniform approaches you again.");
                        Print("\"I apologise for my rash behaviour Captain. I acted on the only evidence that was available. It was in the interest of everyone here. I will relinquish my command of the ship.\"");

                        if (kovaDefeated == true) { Print("\"Kova has been returned to medical. Fortunately his time in stasis left him too weak to phase off the ship before the shields came back online. It appears he was forced to improvise.\""); }

                        Print("The lift opens beside you. Amber Zathre walks onto the bridge and sits at a console. She is followed by Transport Officer Crest who stands looking out of the curved window.");

                        if (kovaDefeated == false && catSeen == true) { Print("Squirming angrily in her arms is the black cat."); }
                        else if (kovaDefeated == false && catSeen == false) { Print("Squirming angrily in her arms is a black cat."); }

                        Print("The computer cuts in.");
                        Print("\"Warp drive coils are at full charge. Resuming previous heading. Standby to jump.\"");
                        Print("Your First Mate looks over from his station.");
                        Print("\"Awaiting your order Captain.\"");

                        while (true)
                        {
                            string i = Prompt();
                            if (i.ContainsAny("engage", "make it so"))
                            {
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }

                        //ending message
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Centered("     >>> CONGRATULATIONS <<<", "\n");
                        Computer("     You have successfully named the ship's crew and discovered your own identity.", "\n");

                        if (kovaDefeated == true)
                        {
                            Computer("                You solved the mystery and defeated Kopos Kova.", "\n\n");
                            Computer("                               Thanks for playing.");
                        }
                        else
                        {
                            Computer("               Unfortunately your prisoner has escaped. Or have they?", "\n\n");
                            Computer("                          Restart the game to play again.");
                        }

                        Console.ReadKey();
                        gameRunning = false;
                        break;

                    /*game over*/
                    case "game over":
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("\n\n");
                        Centered("       >>> GAME OVER <<<", "\n");
                        Centered("   Restart game to try again.");
                        Console.ReadKey();
                        gameRunning = false;
                        break;
                }
            }
        }

    }

}



