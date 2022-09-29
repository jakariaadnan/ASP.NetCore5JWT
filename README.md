# ASP.NetCore5JWT

Step 1:
  Register With Your Name.
  Api: http://localhost:39996/api/auth/Register
  Type: Post
  Json: {
          "UserName":"TestName",
          "Email":"test@gmail.com",
          "Password":"Aa@123456789",
          "Role":"General User",
          "PhoneNumber":"0167487844A",
          "ConfirmPassword":"Aa@123456789"
        }
        
Step 2:
  Log In With User Name And Password. You Get Jwt Token.
  Api: http://localhost:39996/api/auth/login  
  Type: Post
  Json: {
          "Name":"Jakaria",
          "Password":"Aa@123456789",
          "ConfirmPassword":"Aa@123456789"
        }
        
Step 3: Save Product Master And Then Save Product Details With ProductMasterId
  Save Master
  Api: http://localhost:39996/api/Product/SaveUpdateProductMaster
  Type: Post
  Header: Bearer Token
  Json: {
          "id": 0,
          "name": "Fan",
          "sotckDate": "2022-09-09",
          "details": "All fan",
          "status": 1
        }
        
  Save Details
  Api: http://localhost:39996/api/Product/SaveUpdateMasterProductDetails
  Type: Post
  Header: Bearer Token
  Json: [{
            "id": 0,
            "productName": "Fan 12",
            "sotckDate": "2022-09-09",
            "details": "Fan 12 inch",
            "productMasterId": 2
        },
        {   "id": 0,
            "productName": "Fan 14",
            "sotckDate": "2022-09-09",
            "details": "Fan 14 inch",
            "productMasterId": 2
        }]
        
Step 4: Get Product Master And Product Details With ProductMasterId
  Get Master
  Api: http://localhost:39996/api/Product/GetProductMaster
  Type: Get
  Header: Bearer Token
  Responce:
  [
    {
        "id": 2,
        "name": "Fan",
        "sotckDate": "2022-09-09T00:00:00",
        "details": "All fan",
        "status": 1
    }
  ]
  
  Get Detail
  Api: http://localhost:39996/api/Product/GetProductListMyMasterId?id=2
  Type: Get
  Header: Bearer Token
  Responce:
  [
    {
        "id": 1,
        "productName": "Fan 12",
        "productMasterId": 2,
        "productMaster": null,
        "expireDate": null
    },
    {
        "id": 2,
        "productName": "Fan 14",
        "productMasterId": 2,
        "productMaster": null,
        "expireDate": null
    }
  ]
                
        
        
        
        
        
        
        
