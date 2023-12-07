update DbSession
set SessionData = @SessionData, LastAccessed = @LastAccessed, UserId = @UserId
where DbSessionID = @DbSessionID;