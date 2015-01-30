# PaybaseAPI

Simple .NET API for GAW Miner's PayBase
(Include three simple methods to interrogate the Paybase API using a supplied API KEY.

This is a C# project written for Visual Studio 2013 Express. It contains a test application using XAML for portable apps
as in Windows 8.1 and Windows Phone 8.

##Properties

Name|Description
--------|----------------------------------------------------------------------------------------------
Apikey|Sets/Gets the user's API KEY generated within Paybase. Required for all API calls presently.<br>
ApiURI|Sets/Gets the API URI for production or test. Defaults to production API if not specified.<br>

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
Name|Description
-----------------|----------------------------------------------------------------------------------------------
getCurrentUser|Returns unformatted JSON (string) containing user info.<br>
getCurrentUserProfile|Returns unformatted JSON (string) containing user profile info.<br>
getBuysAndSells|Returns unformatted JSON (string) containing recent market buy and sell orders.<br>

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
