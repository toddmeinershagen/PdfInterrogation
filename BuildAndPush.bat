cd .nuget
nuget.exe update -self
ECHO Y | DEL *.nupkg

#Restore packages in solution
nuget.exe restore "..\PdfInterrogation.sln"

nuget.exe setApiKey 565e6305-d5e8-41ec-a70e-494cbb0dc6cf
nuget.exe pack "..\PdfInterrogation\PdfInterrogation.csproj" -Build -Properties Configuration=Release

nuget.exe push *.nupkg
cd ..