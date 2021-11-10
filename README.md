# WikiRip
Program purpose:
The prupose of the program is to take data form wikipedia sites.
The data being links in paragraphs, images links, and to grab paragraphs then search for specific 
words in said paragraphs. It is a simple program; feel free to test the exe, the commands are 
self explained in the nature of what they do, there should be little room for consufion is what I mean.

How the program works:
The program is very simple, There is not a whole lot of complicated background functions. I am using the 
HAP (Html Agility Pack) library. This library is great and it is my main way of getting the specific elements 
from the site that I want to work with. I am grabbing what I want from a specific site (in this case user provided).
What I want being very general elements that I think are going to be in many different wiki sites. I also am grabbing the 
general elements (i.e. //p) at a general xpath and not something specific (i.e. //p/@[class='someclasshere']). Once I 
have the data I am cleaning it or grabbing something from within what I got from the site and presenting that to the user.
To eleborate on the previously stated, I am using substrings and such from c# to grab only the href tag and link in the a 
tag from the html file. I do think I am grabbing the outer html (may be inner) for some elements then using substrings to find
where the html tag is then return it. The reason for doing this is I had issues grabbing the innner html of href links within
a tags; not sure why that was but my solution works quite nicely. When I am returning paragraphs I made an extension method 
that returnes a cleaned string. I am actually stepping though each paragraph and each word then cleaning the words indevidually.
Another option would have been to step through the charagters in the paragraphs or whole text once I filtered what I want to be 
returned. This would have been considerably more work so I decided to use the simpler substring method. Not to say I couldn't have 
done it the harder way but if there is a simpler way to acomplish the same goal then I generally think it should be considered as
a replacement. I will actually be using a version of what I have described above to examin sentances and return only sentances with 
the word instead of the paragraph. Whole bunch of ways I could do it but I am going to do this way.

Go ahead an examin the code. I left verbose comments and did everything so that it would be easyer to understand. There was 
a peice of code I am using from another person for the ascii art, I left codeCredits to him in the comments. Go ahead and check
out the program or download the demo exe I left and give it a try for yourself. Note that the demo exe is DotNetDependant and not 
self contained. So you will need dotnet installed if you are to use it. I do have a self contained exe but it is stored in the cloud
not in this progect. You can create a self contained version yourself since I have included all the visual studio files.

Edits I want to add:
	If no words found then return user to previous text menu.
	Make the window load center to the users screen.
	Make the window a little wider.
	Return only the sentances with the word instead of the paragraphs.
	Give the user an option to save the file to downloads folder or desktop.

If anyone finds any errors please lettme know. 
May decide to leave my email here later on idk.