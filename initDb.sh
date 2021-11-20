#!/bin/bash

echo -e "\n---------------- Add Migrations -------------------"
dotnet ef migrations add InitialCreation -o Data/Migrations

echo -e "\n---------------- Data Base Generating -------------------"
dotnet ef database update