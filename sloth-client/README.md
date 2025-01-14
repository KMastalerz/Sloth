### Sloth

This application is build to manage projects and tasks of various teams. 

### Development Start

## Sloth - Client
prerequisites: 
- Installed Visual Studio Code.

1. Intstall `Node.js`, which can be found at: [Node.js](https://nodejs.org/en/download)
2. Install yarn with `npm install --global yarn`
3. Install angular CLI `npm install -g @angular/cli`
4. Open folder with Visual Studio Code.
5. Run command `yarn` it will install all necessary packages. 
6. Run command `yarn build-all` this is pre-defined script in project, that will build all libraries. 
7. Run command `yarn start` it will compile and run new project. 

For best experience you can also create json file `launch.json` with body (setup for Edge browser): 
```
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": "ng serve msedge",
      "type": "msedge",
      "request": "launch",
      "url": "http://localhost:4200/",
      "webRoot": "${workspaceFolder}"
    }
  ]
}
```

## Sloth - Services
prerequisites: 
- Installed Visual Studio.
- Installed and running MS SQL Server

1. Open solution by clicking `sloth.sln` or select it from IDE.
2. In solution explorer right click on solution and select "Restore NuGet Packages".
3. In solution explorer right click on solution and select "Build" 
4. Modify file: `appsettings.Development.json` for development or `appsettings.json` for production.
5. In selected `appsettings.json` paste your connection string to your database. 
6. Open "Package Manager Console" if not seen on GUI, select Tools -> NuGet Package Manage -> Package Manager Console
7. In "Package Manager Console" select `Sloth.Infrastructure` as deafault project. (migration is located on this layer)
8. In "Package Manager Console" run command: `update-database`.
9. Set `Sloth.API` as startup project. 
10. Press `https` on top panel. 
