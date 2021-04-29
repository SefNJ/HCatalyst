Write-Host "Adding a person with the Last Name of 'Smithson'" -ForegroundColor Green

$Body = @{
    FirstName = "George"
    LastName = "Smithson"
    Street = "10897 South River Front Parkway"
    City = "South Jordan"
    State = "UT"
    Zip = "84095"
    Age = "34"
    Interests = "Stamp collecting, Reading"
}

$Parameters = @{
    Method = "POST"
    Uri =  "https://localhost:44304/api/HCpersons"
    Body = ($Body | ConvertTo-Json) 
    ContentType = "application/json"
}

Invoke-RestMethod @Parameters



Write-Host "Doing a contains search for Last Name 'smith'" -ForegroundColor Green

$Body = @{
    LastName = "smith"
}
 
$Parameters = @{
    Method = "POST"
    Uri =  "https://localhost:44304/api/HCpersons/SearchContains"
    Body = ($Body | ConvertTo-Json) 
    ContentType = "application/json"
}

Invoke-RestMethod @Parameters



Write-Host "Doing an exact search for Last Name 'Smith'" -ForegroundColor Green

$Body = @{
    LastName = "Smith"
}
 
$Parameters = @{
    Method = "POST"
    Uri =  "https://localhost:44304/api/HCpersons/Search"
    Body = ($Body | ConvertTo-Json) 
    ContentType = "application/json"
}

Invoke-RestMethod @Parameters



Write-Host "Doing an exact search for Last Name 'Smith' and First Name 'John'" -ForegroundColor Green

$Body = @{
    LastName = "Smith"
    FirstName = "John"
}
 
$Parameters = @{
    Method = "POST"
    Uri =  "https://localhost:44304/api/HCpersons/Search"
    Body = ($Body | ConvertTo-Json) 
    ContentType = "application/json"
}

Invoke-RestMethod @Parameters
