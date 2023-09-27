## <img src="https://img.shields.io/badge/MySQL-4479A1?style=for-the-badge&logo=MySQL&logoColor=white"> <img src="https://img.shields.io/badge/redis-DC382D?style=for-the-badge&logo=Redis&logoColor=white"> <img src="https://img.shields.io/badge/csharp-239120?style=for-the-badge&logo=CSharp&logoColor=white"> <img src="https://img.shields.io/badge/unity-FFFFFF?style=for-the-badge&logo=unity&logoColor=black">

<img src="https://capsule-render.vercel.app/api?type=waving&color=auto&height=200&section=header&text=GarbageofGalaxy&fontSize=40" />

## 게임 소개
우주의 쓰레기 문제를 해결하기 위해 우주 청소 부대의 일원이 되어 쓰레기를 소멸시키는 2D 탄막 슈팅 게임입니다. <br>
단순한 슈팅 게임이 아니라, 발사한 총알이 목표를 놓치면 플레이어 자신이 위협받게 되는 특별한 시스템을 가지고 있습니다. <br>
전략과 순발력이 중요하며, 향후 멀티플레이 모드를 통해 다른 플레이어와의 대결도 즐길 수 있습니다.<br>
우주의 청소부로서 우주를 지켜보세요!
<br>
<br>

## 브런치 (Branch)
- Unity : Main
- ASP.NET Core : Server
https://github.com/Wally0822/ASP.NETCore_GarbageofGalaxy/tree/Server
<br>

## 씬 대표 화면
- Title Scene
  
- Lobby Scene
- InGame Scene
<br>

## 버전 :
- Unity : 2021.3.7f1
- ASP.NET Core : 7.0 버전
<br>

## 팀원 : 
- 서버 개발 : Wally0822
- 클라이언트 개발 : KingOneChance, kory199, MuveloperDev
<br>

## MySQL Account DB Schema
## Account Table
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
<br>

## 빌드 다운로드 링크 : 
- https://wally0822.github.io/BuildPI/
