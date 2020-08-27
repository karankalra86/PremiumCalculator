# PremiumCalculator : 
This calculates the premium of the member based on Age, Occupation and Sum Insured - For Coding Test

This application is built with multilayerd archirecture with every module performing its responsibility. 

Tools and Technologies Used :
1) ASP.net MVC WebAPI
2) ASP.net MVC Web Application
3) Swagger UI
4) IOC Container - Ninject and Unity
5) TDD - Microsoft unit test framework using Rhino Mocks
5) AutoMapper
6) CORS


Non Functional Requirements Implemented :
1) Logging
2) Exception handling

Assumptions:
1) We have used dictionary collection to act as a data store for Occupations and their rating factors.
2) WebAPi project will be hosted in IIS to be consumed by UI project to make it running.



This solution consists of following projects

1) PremiumCalculatorAPI:
    This is ASP.net MVC WebAPI project integrated with Swaager UI and Unity container. CORS is also implemented, and "*" is configured as origin value for coding test at global level. Swagger UI containes the documentation an testing for the WebAPI operations. 
    
2) PremiumCalculatorAPI.Tests : 
    This is the Unit test project for WebAPI's, built on Microsoft unit test framework consuming Rhino Mock.
    
3) PremiumCalculatorData : 
    This is a class library project used as a Repository pattern, though in this project it is only used for returning Occupation list and Occupation rating factor.
    
4) PremiumCalculatorWrapper :
    This is a class library project that acts as a wrapper/proxy to call PremiumCalculatorAPI.
    
5) PremiumCalcModels :
    This is a class library project created for data models.
    
6) PremiumCalculatorLogger :
    This is a class library project created for common logging, currebtly it is writing the logs in event viewer, if needed this can be extended to handle file writes and sql        logging as well.
    
6) PremiumCalculatorUI :
    This is ASP.net MVC Web Application created for the UI logic. It consumes Ninject container for IOC. AutoMapper is used to map the Viewmodels with data model.
    
7) PremiumCalculatorUI.Tests :
    This is the Unit test project for UI, built on Microsoft unit test framework consuming Rhino Mock.
