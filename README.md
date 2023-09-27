## <img src="https://img.shields.io/badge/MySQL-4479A1?style=for-the-badge&logo=MySQL&logoColor=white"> <img src="https://img.shields.io/badge/redis-DC382D?style=for-the-badge&logo=Redis&logoColor=white"> <img src="https://img.shields.io/badge/csharp-239120?style=for-the-badge&logo=CSharp&logoColor=white"> <img src="https://img.shields.io/badge/unity-FFFFFF?style=for-the-badge&logo=unity&logoColor=black">

<img src="https://capsule-render.vercel.app/api?type=waving&color=auto&height=200&section=header&text=GarbageofGalaxy&fontSize=40" />

## 브런치 (Branch)
- Unity : Main
- ASP.NET Core : Server
https://github.com/Wally0822/ASP.NETCore_GarbageofGalaxy/tree/Server
<br>

## 버전 :
- Unity : 2021.3.7f1
- ASP.NET Core : 7.0 버전
<br>

## 팀원 : 
- 서버 개발 : Wally0822
- 클라이언트 개발 : KingOneChance, kory199, MuveloperDev
<br>

## MySQL Schema
﻿# [ Account DB ]
  
## account Table

```sql
DROP DATABASE IF EXISTS accountDb;
CREATE DATABASE IF NOT EXISTS accountDb;

USE accountDb;

DROP TABLE IF EXISTS accountDb.`account`;
CREATE TABLE IF NOT EXISTS accountDb.`account`
(
    account_id BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT '계정번호',
    id VARCHAR(50) NOT NULL UNIQUE COMMENT '아이디',
    salt_value VARCHAR(100) NOT NULL COMMENT  '암호화 값',
    hashed_password VARCHAR(100) NOT NULL COMMENT '해싱된 비밀번호',
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '생성 날짜'
) COMMENT '계정 정보 테이블';
```
- GameDataBase : https://github.com/Wally0822/ASP.NETCore_GarbageofGalaxy/blob/Server/APIServer/DbSchema/GameDB.md
- MasterData : https://github.com/Wally0822/ASP.NETCore_GarbageofGalaxy/blob/Server/APIServer/DbSchema/MasterData.md

## 빌드 다운로드 링크 : 
- https://wally0822.github.io/BuildPI/
