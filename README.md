SwagLabs Shopping Cart Tests
============================

## Prerequisite
 - Visual Studio 2022 Community (recommended)
 - .NET SDK 6.0
 - .NET Runtime 6.0
 - ASP.NET Core Runtime 6.0
 - Selenium Chrome Driver and browser

## Introduction
SwagLabs Shopping Cart is a test project that executes TDD and BDD Selenium test cases/scenarios on https://www.saucedemo.com website.

## Project Structure
Solution is comprised from following folders and few general C# classes:
- Features: contains BDD feature files (Gerkins) and auto generated corresponding classes
- Models: contains CSS selector classes from POM (Page Object Model) design pattern. It also holds domain classes like Product as a general abstraction from HTML item representation
- Pages: contains specific POM realization for each individual page
- StepDefinitions: contains binding classes to BDD Gerkin scenarios
- Tests: contains specific TDD test cases for each page

## Running the tests
In Visual Studio Test Explorer (Windows) / Test view (Mac) Run SwagLabs_Shopping. This will trigger all feature (BDD) and TDD tests

## Notes
Tests are executable with different users. Some of the methods are executed asynchronous to cover the 'performance_glitch_user'. 
To run the tests with 'problem_user' you should uncomment the coresponding TestCase in BaseTest.cs file. Some of the test will fail with this user because there are bugs.
