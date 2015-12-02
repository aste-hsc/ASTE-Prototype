# ASTE-Prototype
Prototype for ASTE -platform

###Installation:

####Step 1: Install PostgreSQL ([http://www.postgresql.org])
####Step 2: Install JRE ([http://www.oracle.com/technetwork/java/javase/downloads/jre8-downloads-2133155.html])
####Step 3: Start the LoggerModule on console:

    example:
    java -Dserver.port=8090 -Dserver.contextPath=/loggerModule -jar logger-module-1.0-SNAPSHOT.jar

    Dserver.port -> the Port you wish to run LoggerModule
    Dserver.contextPath -> Url prefix (in the example it would be http://localhost:8090/loggerModule)

####Step 4: fix the web.config connectionstring values for the following C# projects:

        ASTE.Modules.APIDiscovery:
        <add name="APIDiscoveryContext" connectionString="Server=[PostgreSQL ip];Port=[PostgreSQL port];User Id=[PostgreSQL user];Password=[PostgreSQL password];Database=APIDiscovery" providerName="Npgsql" />

        example:
        <add name="APIDiscoveryContext" connectionString="Server=127.0.0.1;Port=5432;User Id=postgres;Password=asteuser;Database=APIDiscovery" providerName="Npgsql" />
    
        ASTE.Modules.FormModule
        <add name="FormModuleContext" connectionString="Server=[PostgreSQL ip];Port=[PostgreSQL port];User Id=[PostgreSQL user];Password=[PostgreSQL password];Database=ASTE" providerName="Npgsql" />

        example:
    	<add name="FormModuleContext" connectionString="Server=127.0.0.1;Port=5432;User Id=postgres;Password=asteuser;Database=ASTE" providerName="Npgsql" />

####Step 5: Change the API Discovery port to the following projects web.config:
    	Change the API Discovery port to the following projects web.config:
    	
    	ASTE.Public.Rest
		ASTE.Modules.FormModule
		ASTE.Processes.Mielenterveysseura
	
		<appSettings>
    		<!-- ASTE API Discovery url -->
    		<add key="API_Discovery_Url" value="http://localhost:[YOUR PORT]" />
  		</appSettings>

    
####Step 6: Host the following C# projects to IIS:

	ASTE.Demos.Mielenterveysseura
	ASTE.Modules.APIDiscovery
	ASTE.Modules.FormModule
	ASTE.Processes.Mielenterveysseura
	ASTE.Public.Rest
	

####Step 7: adding modules and processes to the API Discovery:
	
	1. Navigate to the url in which you installed ASTE.Modules.APIDiscovery	¨
	2. Installing LoggerModule:
		2.1 click modules
		2.2 click add new module
			- url: the url you defined in step 3, example http://localhost:8090/loggerModule
		        - version: 1.0
		2.3 click add
		    
		if everything went fine, you should be redirected to the modules index page.
		
		2.4 Click Edit on the Logger module row
		2.5 Check Active, and click Update on the bottom of the page
		
		Page should redirect back to module index page, and on the LoggerModule, Active should be true
		
	3. Installing ASTE.Modules.FormModule
		
		3.1 In the API Discovery Root, select modules and add a new module
		  
		  url: http://localhost:[The Port you assigned in IIS]/FormModule
		  version: "1.0"
		  
		3.2 Activate module through Edit

	4. Installing ASTE.Processes.Mielenterveysseura
		4.1 In the API Discovery Root, select processes and add a new process
		  
		  url: http://localhost:[The Port you assigned in IIS]/Mielenterveysseura
		  version: "1.0"
		  
		4.2 Activate process through Edit


####Step 8: Web application configurations (ASTE.Demos.Mielenterveysseura)

		1. In API Discovery, select Clients.
		2. Add a new Client and click add, example:
		   Name: Toni Iltanen
		   Client ip: 127.0.0.1
		   Client name: Test app
		
		3. copy the new api key from the client index page, and paste it to 
		
		ASTE.Demos.Mielenterveysseura/Views/Shared/_Layout.cshtml, in line number 204
		  
		  example:
		          $.ajaxSetup({
		            beforeSend: function (xhr) {
		                xhr.setRequestHeader('api_key', 'your-new-api-key');
		            }
		        });
		        
		4. Change the public rest api url (ASTE.Public.REST) in the following positions, to the one you installed it on IIS
		
		ASTE.Demos.Mielenterveysseura/Views/Home/Index.cshtml, in line number 38
		    url: "http://localhost:[Your port]/public/1.0/Get",
		    
		ASTE.Demos.Mielenterveysseura/Views/Home/Index.cshtml, in line number 73
		    url: "http://localhost:[Your port]/public/1.0/Post",
		    
		ASTE.Demos.Mielenterveysseura/Views/Home/Index.cshtml, in line number 87
		    url: "http://localhost:[Your port]/public/1.0/Get",
    
####Step 9: Navigate to the demo web application by adding /lapset-puheeksi/lokikirja to your ASTE.Demos.Mielenterveysseura site on iis:
	
	example http://localhost:55708/lapset-puheeksi/lokikirja




