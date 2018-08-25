# DuckQL

This is my first projekt. It's primary for learning coding and algorithms. 
I develop this In my spare time and personally used it on some occasions in my work environment.
I'LL GIVE NO WARRANTY!! YOU MAY USE IT AT YOUR OWN RISK!! 
This program reads data from one database and puts it into another.
Common usecases: Migrating data into a new schema, sync data between two independent datasources.
Development: I'll add some more data adapters from time to time and try to optimize performance.
I'm looking forward to your addtions, comments or ideas :)

The program is useable from the command line. The core function (transfering data from one db to another) is configured by an xml file.
To create such an xml file a added a gui, so you can setup your configuration without reading 200 pages of documentation (that I don't 
have to write).

To start the configuration GUI use the parameter -c, i.e. [Path\]sqlduck.exe -c. If you have a preconfigured xml file you can directly run
the program by using the path to your xml (folder), i.e. [Path\]sqlduck.exe [PATH] or more specific: 
C:\Users\waterkantnerd\sqlduck.exe "C:\Users\waterkantnerd\sqlduck\My SQLDuck XML-Jobs\"
