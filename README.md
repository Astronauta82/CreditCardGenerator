# CreditCardGenerator

Like the name of the repository says, this project is a Credit Card Generator example. The purpose of this repository is give you an example of how to run an Azure Function Project in a Docker Container. 

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. 

### Introduction
The Credit Card Generator example is based on [Luhn algorithm](https://en.wikipedia.org/wiki/Luhn_algorithm). As an example project, it has only 4 types of credit card mocked so you can run a few test with postman. If you want to add a DataAcces layer to avoid the mock, feel free to do that.

Credit Card Types:
1. Mastercard
2. Visa
3. American Express
4. Cabal

To test with postman you must send through POST method the following json:
```
{
	'TypeId':'2',
	'CustomPrefix':''
}
```
Where TypeId is the credit card type, and CustomPrefix, like his name says, is a custom prefix for the credit card number. If CustomPrefix is empty, the credit card generator will take the predifined prefix for the credit card type.

### Prerequisites
This example was developed on macOS Mojave. If you want to run it in other platform, please install the corresponding software.

Install the following software:
* [Visual Studio Code](https://code.visualstudio.com/download)
* [C# for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp)
* [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local#v2)
* [Docker](https://hub.docker.com/editions/community/docker-ce-desktop-mac)
* [Postman](https://www.getpostman.com/downloads/)


To install Azure Functions Tools you must install in the following order:
1. Install [.NET Core 2.x SDK for macOS.](https://dotnet.microsoft.com/download)
2. Install [Homebrew](https://brew.sh), if it's not already installed.
3. Install the Core Tools package:
```
brew tap azure/functions 
brew install azure-functions-core-tools

```

### Installing

Now that you have setting environment prepared:
1. Clone this repo.
2. Open the project folder with visual studio code
3. Open a terminal and execute the following commands:
```
dotnet restore
dotnet build
```
To create a docker image:
```
docker build -t <imagename>:<tag> .
```
To instance the container:
```
docker run -p 8080:80 <imagename>:<tag> 
```
## Example
1. Open postman
2. Select POST as HTTP Verb copy and paste the application url http://localhost:8080/api/GenerateCreditCard
3. In the Body section, select the option Raw and the type Json.
4. Copy the following json and paste it in the body section, then click Send.
```
{
	'TypeId':'2',
	'CustomPrefix':''
}
```




## Authors

* **Jonatan Medinilla** 


## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

