# Azure Text Analytics Tester

This is a basic dotnet core console application used to test out hte Azure Text Analysis api. Current it only
has support for the sentiment analytics.

## Prerequisites

> .NET Core - Follow the instructions for your platform [here](https://www.microsoft.com/net/core#windows)
> Azure Text Analytics Account Key - Folow the instructions [here](https://www.microsoft.com/cognitive-services/en-us/text-analytics-api) to set one up in Azure

## Getting started

Clone the repository and restore packages:

```sh
git clone https://github.com/justsayno/AzureTextAnalyticsTester
cd AzureTextAnalyticsTester
dotnet restore
```

Add your Text Analytics api key as a user secret:

```sh
dotnet user-secrets set TextAnalytics:AzureSubscriptionKey <api-key-here>
```

Run the application:

```sh
dotnet run
```

Or use Visual Studio 2015
