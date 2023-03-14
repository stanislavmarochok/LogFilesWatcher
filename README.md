# LogFilesWatcher

# Introduction
On this GitHub repository you can find a source code of the software LogFilesWatcher, created by Stano Marochok on 3/14/2023 within an interview process.
The application is implemented in .NET 6. It uses MVC pattern for the workflow management.

# How to use it?
First of all, download the entire code from the repository.
Then, build the solution.
Then, run the build .exe file.

# User documentation
When you started the program, you will see such a screen:
![image](https://user-images.githubusercontent.com/32093806/225008014-60d4cb34-6f85-4cb0-b62b-2fe2361d2049.png)

Press on the "Browse" button to choose a directory you would like to analyze.
Then, select the directory.
Then, press big green button signed as "Press to check if the filesystem was updated".
You should receive a status of you selected directory.

Use button below to execute what is written on them.

# For devs
MVC design pattern is implemented within the software. Also, a lot of optimization techniques are used as well (see class HistoryItemsController.cs). Honestly, I think the code is written in a such way that anybody can understand what is happening there.

Have a good time!
