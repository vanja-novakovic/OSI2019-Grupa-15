Initial configuration: In folder Database/Config there is file named Properties.settings, and
you set your connection string there. In folder Database/Scripts there is CreateEve.sql file that is used
to create database. Just run that script in MySQLWorkbench, and database will be created. Then, in same folder,
there is InsertScriptContent file with no extension, that you need to copy in query window in MySQLWorkbench to 
populate database. Your pair (username, password) is (Admin, admin). You can use this to log in. 
You need to have .NET Framework 4.7.2 installed on your machine. Run it in Visual Studio.

About tehnologies: WPF is used, with .NET Framework libraries, and three layer archiecture. MySQLClient is used, not Entity Frawework.
Also, reflection is used to cut down time for application development and learn something new, although is slow in .NET,
so if you want better performance, you need to change code logic. But paging is used, as well as async, as performance was 
also important.
