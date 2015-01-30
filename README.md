# PaybaseAPI

Simple .NET API for GAW Miner's PayBase
(Include three simple methods to interrogate the Paybase API using a supplied API KEY.

##Properties

Apikey  Sets/Gets the user's API KEY generated within Paybase. Required for all API calls presently.
ApiURI  Sets/Gets the API URI for production or test. Defaults to production API if not specified.

###Example:
```
   //  instantiate wrapper instance 
   var paybase = new PayBaseWrapper.PayBase();

   // insert your key 
   paybase.Apikey = apikey; 

   // defaults to production
   paybase.ApiURI = apiuri; 
   
   // do some work
```
##Methods
   getCurrentUser         Returns unformatted JSON (string) containing user info.
   getCurrentUserProfile  Returns unformatted JSON (string) containing user profile info.
   getBuysAndSells        Returns unformatted JSON (string) containing recent market buy and sell orders.

###Example:
```
  var result = "";
  
  var paybase = new PayBaseWrapper.PayBase();

  paybase.Apikey = "YOUR COMPLEX API KEY"; 

  result = paybase.getCurrentUser(); 

  result = paybase.getCurrentUserProfile(); 

  result = paybase.getBuysAndSells(); 

  paybase = null; 
```
