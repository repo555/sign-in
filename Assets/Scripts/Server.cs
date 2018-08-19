using System;
using UnityEngine;

public static class Server
{
    public static void SignIn(string emailAddress, string password, Action<string> onComplete)
    {
		if(emailAddress == "" || password == "")
            onComplete(null);
        else if(emailAddress == "skyvr%40sky.com" && password == "v1rtualr3al%21ty")
            onComplete("{ status: \"valid\", token: \"1da3c206-aeab-4570-b590-5b421237eafa\" }");
		else
			onComplete("{ status: \"false\", reason: \"Invalid credentials provided\" }");
    }
}
