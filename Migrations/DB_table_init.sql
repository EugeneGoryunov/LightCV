create table if not exists AppUsers (
    UserId serial primary key,
    Email varchar(100),
    Password varchar(100),
    Salt varchar(100),
    Status int
);

create table if not exists UserSecurity(
    UserSecurityId serial primary key,
    UserId int,
    VerificationCode varchar(50)
);

create table if not exists EmailQueue(
    EmailQueueId serial primary key,
    EmailTo varchar(200),
    EmailFrom varchar(200),
    EmailSubject varchar(200),
    EmailBody text,
    Created time,
    ProcessingId varchar(100),
    Retry int
);

create table if not exists Profile
(
    ProfileId serial primary key,
    UserId int,
    ProfileName varchar(50),
    FileName varchar(50),
    LastNmew varchar(50),
    ProfileImage varchar(100)
)