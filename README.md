## Gringotts Bank
- [About](##About)
- [Setup](##Setup)
- [Solution Architecture](##Architecture)
- [Getting Authentication Token](##Getting-Authentication-Token)
- [List of APIs](##List-of-APIs)
- [Database Used](##Database-Used)
- [Packages Used](##Packages-Used)

# About
Gringotts Bank is a bank that has an online branch for wizards to do some account transactions.

## Setup
1. Clone the repository: git clone https://github.com/github-brij-mohan/gringotts-bank.git.
2. Change directory into the cloned repository cd Gringotts.
3. Open the **Gringotts.sln** in Visual Studio.
4. Open Startup.cs class and at  line number - 81 change current connection string to your MSSql server connection string.
6. Hit F5 to start the application.
7. When you hit any api this will create a database named gringotts-bank.db automatically in your workspace.

## Architecture
- Solution Architecture: 
  - Solution layers and their responsibilities:
    - Host Layer
      - Controller - Responsible for getting request and response from the external user.
      - Service - Responsible for host level validations (request validation) and bussiness model translation.
    - Core Layer
      - Models - Holds all business level models and interfaces.
      - Core - Holds all bussiness logic of the system.
    - Repository Layer
      - Repository - An abstraction layer above Dal which is responsible for gettig response from data base and data entities model translation.
    - Dal Layer
      - Dal - Expands as Data Access Layer which is responsible for data base communication.
    - Test Layer
      - Tests - Holds tests for all layers
  - An Api call flow in Gringotts Bank application:
  - ![image](https://user-images.githubusercontent.com/85063157/155066241-ff38063b-43da-403d-8ac6-9a41d27518e9.png)
- Database Architecture:
  - Tables:
    - Customer - Holds customer Data
    - Account - Holds Account Data
    - Transaction - Holds Transaction Data  
      ![image](https://user-images.githubusercontent.com/85063157/155064749-c2cf8e44-35bc-45ea-8707-479c2c074a62.png)

## Getting Authentication Token
- Open Postman import below curl and send the request, you will get access token with some expiry time in the response:
- > curl --location --request POST 'https://dev-augl2m5i.us.auth0.com/oauth/token' \
    --header 'content-type: application/json' \
    --header 'Cookie: did=s%3Av0%3A93253f50-9255-11ec-89c3-2bff1c996735.YFlKHK1vsrrmsjgatBJT3jdqcezFEFKTA2E1qrh%2B10Y; did_compat=s%3Av0%3A93253f50-9255-11ec-89c3-2bff1c996735.YFlKHK1vsrrmsjgatBJT3jdqcezFEFKTA2E1qrh%2B10Y' \
    --data-raw '{
        "client_id": "oVOmthwVrsMJKYgPRzdnfBP7i4mySuzZ",
        "client_secret": "iYpMa0nFxwntlwuUwrDIKq7sjb7d4jFxLJJvb1JbHLdgJOp1HigW_WtKordCpumI",
        "audience": "https://localhost:44311/",
        "grant_type": "client_credentials"
    }'

## List of APIs
- Customer
  - Create Customer-
    > Url - /api/v1.0/customers <br />
    > Verb - POST <br />
    > Action - Create Customer <br />
    >
    > Request Body - <br />
    > { <br />
    >   "name": "name of customer,<br />
    >   "address": "address of customer",<br />
    >   "mobile": "mobile number of customer",<br />
    >   "email": "email id of customer"<br />
    > }<br />
    ><br />
    > Headerds -<br />
    > Authorization - Bearer {bearer token}<br />
    
  - Get Custoemr By Id -
    > Url - /api/v1.0/customers/{customerId} <br />
    > Verb - GET <br />
    > Action - Get Customer by Customer Id <br />
    ><br />
    > Request Parameters -<br />
    > customerId <br />
    >   - Mandatory <br />
    >   - Data Type - int <br />
    > <br />
    > Headerds -<br />
    > Authorization - Bearer {bearer token}<br />


- Account
  - Create Account-
    > Url - /api/v1.0/customers/{customerId}/accounts <br />
    > Verb - POST <br />
    > Action - Create Account <br />
    > 
    > Request Parameters -<br />
    > customerId <br />
    >   - Mandatory <br />
    >   - Data Type - int <br />
    > <br />
    > 
    > Request Body - <br />
    > { <br />
    >     "type": "type of account either savings or current",
    >     "currency": "currency of money eg. INR"
    > }
    ><br />
    > Headerds -<br />
    > Authorization - Bearer {bearer token}<br />

  - Get By Id -
    > Url - /api/v1.0/customers/{customerId}/accounts/{accountId} <br />
    > Verb - POST <br />
    > Action - Get Account By Id <br />
    > 
    > Request Parameters -<br />
    > customerId <br />
    >   - Mandatory <br />
    >   - Data Type - int <br />
    > <br />
    > accountId <br />
    >   - Mandatory <br />
    >   - Data Type - int <br />
    > <br />
    > Headerds -<br />
    > Authorization - Bearer {bearer token}<br />

  - Get All Accounts of a Customer -
    > Url - /api/v1.0/customers/{customerId}/accounts <br />
    > Verb - POST <br />
    > Action - Get All Accounts <br />
    > 
    > Request Parameters -<br />
    > customerId <br />
    >   - Mandatory <br />
    >   - Data Type - int <br />
    > <br />
    > 
    > Headerds -<br />
    > Authorization - Bearer {bearer token}<br />


- Transaction
  - Create Account-
    > Url - /api/v1.0/customers/{customerId}/accounts/{accountId}/transactions <br />
    > Verb - POST <br />
    > Action - Create Transaction <br />
    > 
    > Request Parameters -<br />
    > customerId <br />
    >   - Mandatory <br />
    >   - Data Type - int <br />
    > <br />
    > accountId <br />
    >   - Mandatory <br />
    >   - Data Type - int <br />
    > <br />
    > Request Body - <br />
    > {<br />
    >   "amount": { <br />
    >     "currency": "currency of transaction eg. INR", <br />
    >     "value": 0 // amount of transaction <br />
    >   },
    >   "type": "type of transaction either 'Withdraw' or 'Deposit'", <br />
    >   "description": "description of transaction" <br />
    > } <br />
    ><br />
    > Headerds -<br />
    > Authorization - Bearer {bearer token}<br />

  - Get By Id -
    > Url - /api/v1.0/customers/{customerId}/accounts/{accountId}/transactions/{transactionId} <br />
    > Verb - POST <br />
    > Action - Get Transaction By Id <br />
    > 
    > Request Parameters -<br />
    > customerId <br />
    >   - Mandatory <br />
    >   - Data Type - int <br />
    > <br />
    > accountId <br />
    >   - Mandatory <br />
    >   - Data Type - int <br />
    > <br />
    > transactionId <br />
    >   - Mandatory <br />
    >   - Data Type - int <br />
    > <br />
    > Headerds -<br />
    > Authorization - Bearer {bearer token}<br />

  - Get All Transactions of an account -
    > Url - /api/v1.0/customers/{customerId}/accounts/{accountId}/transactions <br />
    > Verb - POST <br />
    > Action - Get All Accounts <br />
    > 
    > > Request Parameters -<br />
    > customerId <br />
    >   - Mandatory <br />
    >   - Data Type - int <br />
    > <br />
    > accountId <br />
    >   - Mandatory <br />
    >   - Data Type - int <br />
    > <br />
    > Headerds -<br />
    > Authorization - Bearer {bearer token}<br />

## Database Used
- MSSql

## Packages Used
- Fluent Validation - For service layer validations.
- xUnit - For writing unit test cases.
- Fluent Assertions - For assertions.
