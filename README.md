# iso3
isomorphic source code analyse and synthesis

## .NET Roslyn Parser for Arango Graph Database

This project provides a .NET Roslyn-based parser that extracts Abstract Syntax Trees (AST) of every class in a solution. The parsed information is then stored in an Arango Graph Database, with inter-class edges representing method calls.

## Table of Contents

1. [Introduction](#introduction)
2. [Features](#features)
3. [Prerequisites](#prerequisites)
4. [Installation](#installation)
5. [Usage](#usage)
6. [Configuration](#configuration)
7. [Contributing](#contributing)
8. [License](#license)

## Introduction

This parser leverages the power of .NET Roslyn to analyze the source code of a given solution and extracts the AST for each class. The ASTs, along with inter-class relationships based on method calls, are then stored in an Arango Graph Database.

## Features

- **AST Extraction:** Generates Abstract Syntax Trees for every class in a .NET solution using Roslyn.
- **Graph Database Storage:** Stores the parsed ASTs and inter-class method call relationships in an Arango Graph Database.
- **Inter-Class Edges:** Establishes edges between classes based on method calls, creating a comprehensive representation of the codebase.

## Prerequisites

- **.NET SDK:** Ensure that you have the .NET SDK installed on your machine. If not, you can download it [here](https://dotnet.microsoft.com/download).

- **ArangoDB:** Set up an instance of ArangoDB. You can download ArangoDB [here](https://www.arangodb.com/download/).

## Installation

1. **Clone the repository:**

    ```bash
    git clone https://github.com/yourusername/roslyn-parser-arango.git
    ```

2. **Navigate to the project directory:**

    ```bash
    cd roslyn-parser-arango
    ```

3. **Restore dependencies:**

    ```bash
    dotnet restore
    ```

## Usage

1. **Configure the AppSettings:**

    - Open the `appsettings.json` file and configure the ArangoDB connection settings.

2. **Run the Parser:**

    ```bash
    dotnet run -- --solutionPath YourSolution.sln
    ```

    - Replace `YourSolution.sln` with the path to your solution file.

3. **View the Graph Database:**

    Access the ArangoDB web interface to explore the stored ASTs and inter-class relationships.

## Configuration

The `appsettings.json` file contains configuration settings for the ArangoDB connection. Update these settings to match your ArangoDB instance.

```json
{
  "ArangoDB": {
    "Url": "http://localhost:8529",
    "Database": "YourDatabaseName",
    "Username": "YourUsername",
    "Password": "YourPassword"
  }
}


[SQL AST](http://ns.inria.fr/ast/sql/index.html)
[Static analysis of a code in a graph database ](https://greenspector.com/en/articles/2017-06-12-analyse-statique-code-bdd-orientee-graphe/)
[Getting Started C# Syntax Analysis](https://github.com/dotnet/roslyn/wiki/Getting-Started-C%23-Syntax-Analysis)
[ArangoDB Graphs](https://docs.arangodb.com/3.3/Manual/Graphs/)
[cudaHopfield](https://github.com/dariosharp/cudaHopfield)
[PlaidML](https://www.intel.ai/plaidml)
[Что такое логическое программирование и зачем оно нам нужно](https://habr.com/ru/post/322900/)
[Использование графов для решения разреженных систем линейных уравнений](https://habr.com/ru/post/438716/)

arangodb
127.0.0.1:8529

[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/epirogov)

[Computer Scientists Inch Closer to Major Algorithmic Goal]
(https://www.quantamagazine.org/computer-scientists-inch-closer-to-major-algorithmic-goal-20230623/)


Vector Database for LLM applications
There are many options out there:
- FAISS

- Pinecone

- Milvus

- Qdrant

- Weaviate

- Elasticsearch

- Vespa

- pgvector

- ScaNN

- Vald
