# This process is broken down into 2 steps

### Step 1
# Creates a layer from dotnet:2.2-sdk
FROM microsoft/dotnet:2.2-sdk AS betting-env

# Create app directory from inside the docker image
WORKDIR /app

# Copy all code into the root directory created above
COPY ./code ./

# Install all dependencies defined in above copied folder
RUN dotnet restore

# Publish respective projects into respective out folder
RUN dotnet publish -c Release -o out


### Step 2
# Creates a layer from dotnet:2.2-aspnetcore-runtime
FROM microsoft/dotnet:2.2-aspnetcore-runtime

# Overide default behavior of asp dotnet apps by setting ASPNETCORE_URLS
ENV ASPNETCORE_URLS=http://+:3000

# Create app directory from inside the docker image
WORKDIR /app

# Copy all the artifacts from previous build into the root directory created above i.e. STEP 2
COPY --from=betting-env /app/BettingAggregator.Api/out ./

# Expose 3000 as port
EXPOSE 3000

# Entry point that will start the Betting Aggregator in Docker Container
ENTRYPOINT ["dotnet", "BettingAggregator.Api.dll"]