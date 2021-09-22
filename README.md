# TechnicalShowCase_LT_PhotoAlbum (.NET 5)

## Introductions
Hello, this application showcases a possible solution on how to pull information from an API endpoint.

## Requirements
 - [.Net 5](https://dotnet.microsoft.com/download/dotnet/5.0)
 - Code IDE
	- [Visual Studio](https://visualstudio.microsoft.com/downloads/) (Optional)
		- When downloading visual studio it can install .net 5 at that time as well.

## Development / Setup
1. Clone the repo down.
1. Navigate to the .sln location inside of your file explorer where the project was cloned to. EX. c:\users\USERNAME\source\repos\TechnicalShowCase_LT_PhotoAlbum\src
1. After the application is open in Visual Studio, right click on the "Solution 'LT_PhotoAlbum' (2 of 2 projects)" in the solution explorer
1. Click on the 'Restore Nuget Packages' from the list.
1. That should be everything you need to do in order to start coding in the application!!

## Console
### Building
1. Navigate to the project directory where the repo was cloned to. 
  1. Starting from the drive letter Ex. ```cd users\USERNAME\source\repos\TechnicalShowCase_LT_PhotoAlbum```
1. Run ```dotnet build```

### Testing
1. Navigate to the project directory where the repo was cloned to. 
  1. Starting from the drive letter Ex. ```cd users\USERNAME\source\repos\TechnicalShowCase_LT_PhotoAlbum```
1. Run ```dotnet test```

## Commands
- album		 Display available albums.
  - \-i, --AlbumId    Required. Filter on a given album id.
- photo      Display all available photos for a given album.
  - \-i, --AlbumId    Required. Filter on a given album id.
  - \-p, --PhotoId    Filter on a optional photo id.
- help       Display more information on a specific command.
- version    Display version information.
- exit       Exits the application

### Command Examples
Get the album with an Id of 2
```> ablum --AlbumId=2```
OR
```> ablum -i2```

Get a photo from an album
```> photo -i2 -p53```