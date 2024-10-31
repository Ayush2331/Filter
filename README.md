Overview
This API provides a flexible way to filter and sort products based on various criteria.
Request Structure
Endpoint: /api/Filter 
Method: POST
Request Body:
JSON
{
    "name": ["Product1", "Product2"],
    "type": ["TypeA", "TypeB"],
    "stocksPurchased": [10, 20],
    "mfgStartDate": "2023-11-01",
    "mfgEndDate": "2023-12-31",
    "sortRequest": {
        "isRelevant": true,
        "isDateAscending": null,
        "isPopularAscending": null
    },
    "paginationRequest": {
        "page": 1,
        "pageSize": 10
    }
}
Explanation of Request Parameters:
Filtering:
•	name: A list of product names to filter by(please send null if don’t want filter on this).
•	type: A list of product types to filter by(please send null if don’t want filter on this).
•	stocksPurchased: A list of stock purchase quantities to filter by(please send null if don’t want filter on this)..
•	mfgStartDate: The start date for manufacturing date filtering(please send null if don’t want filter on this)..
•	mfgEndDate: The end date for manufacturing date filtering(please send null if don’t want filter on this).
Sorting: 
Relevance: Sorts results based on a combination of factors like recently manufactured and popularity.
Date: Sorts results by manufacturing date in ascending or descending order.
Popularity: Sorts results by popularity (Stock Purchased) in ascending or descending order.
•	sortRequest: 
o	isRelevant: Sort by relevance (default: false).
o	isDateAscending: Sort by manufacturing date in ascending order (default:null ; Ascending:true ; Descending:false).
o	isPopularAscending: Sort by popularity in ascending order ((default:null ; Ascending:true ; Descending:false).
Pagination: 
Controls the number of items returned per page and the current page number.
•	paginationRequest: 
o	page: The current page number.
o	pageSize: The number of items per page.
Response Structure:
JSON
{
    [
        {
            "name": "ProductA",
            "type": "TypeA",
            "stockQuantity": 100,
            "mfgDate": "2023-11-15"
        },
        // ... other products
    ],
}

Response sends the name, type, stock quantity, mfg date of the filtered products.


Example Usage:
To filter products by name and sort them by relevance in descending order, you would send a request like this:
JSON
{
  "name": ["ProductA"],
  "sortRequest": {
    "isRelevant": true
  }
}


Note:-

Authentication & Authorization
This API requires users to be authenticated and authorized before accessing the Filter API. The following steps outline the process:
1. User Registration:
Endpoint: /api/Auth/register 
Method: POST
Request Body:
JSON
{
  "username": "user@example.com",
  "password": "strong_password",
  "roles": ["Writer"] // Optional list of user roles
}

Response:
•	Upon successful registration, the API returns a success message (e.g., HTTP status code 201 Created).
•	In case of errors (e.g., username already exists), the API returns an error message with an appropriate status code (e.g., 400 Bad Request, 409 Conflict) and details about the error.
2. User Login:
Endpoint: /api/Auth/login 
Method: POST
Request Body:
JSON
{
  "username": "user@example.com",
  "password": "strong_password"
}

Response:
•	On successful login, the API returns a JSON response containing a JWT token in the Authorization header or the body.
•	The token needs to be stored securely on the client-side (e.g., localStorage) for subsequent API requests.
•	If the login fails (e.g., invalid credentials), the API returns an error message with an appropriate status code (e.g., 401 Unauthorized).
3. Accessing Filter API:
Endpoint: /api/Filter 
Method: POST
Request Headers:
•	Include the Authorization header with the JWT token obtained from the login API.
Example:
Authorization: Bearer <your_jwt_token_here>
Request Body:
(Same as described in previous sections)
Response:
•	The API returns the filtered and sorted product data if the request is successful and the user is authorized.
•	If the request is unauthorized (missing or invalid token), the API returns a 401 Unauthorized error message.

