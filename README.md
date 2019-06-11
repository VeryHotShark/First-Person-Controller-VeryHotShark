# First Person Controller VeryHotShark
The goal of this project is to make a First Person Controller that gives better experience than default FPS controller provided by Unity in Standard Assets.

I will be adding more features to this project in future, but keep in mind that this Controller was made and is being made to suits the best for my Walking Simulator.

This is not some kind of Asset, but maybe i will make some kind of Ultimate Controller from this in future but not for now.

I share this because i hope that this can be a good reference for all people that wants to also make their own Controller for their games.

I will also publish Tutorial Series from this controller on my Youtube Channel : **VeryHotShark**.
So anyone who wants to understand this code better, you should check it out :)

Last thing for now is that i also used a free extension from Asset Store called Naughty Attributes and it is included in this project and ProBuilder as well because i used it to block out some basic prototype scene.

# HOW TO USE IT
So in order to use it in different scenes you have to make sure that you have few things
1. There should be Input Handler script in order to make input working, you should drag the input data throught inspector
2. Make sure to layer your Ground to be on layer that player is colliding with
3. Make sure to have all Data dragged into correct scripts

Im using scriptable objects to store data input so i can easily drag asset to script i want and use it data.
To fill out data i use Input Handler script which basically updates the input.
