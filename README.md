# Pleiades
> #### 사용언어 및 툴 : C#, Unity
> #### 작업 인원 : 1인 프로젝트

--------
## 게임 기획
### 게임 기본 컨셉
> ###### 대상 플레이어 : 어렵지 않고 간편한 게임을 좋아하고 소소한 힐링을 원하는 유저층
> ###### 게임 플랫폼 : iOS, Android
> ###### 게임 장르 : 식당을 운영하고 스토리를 감상하는 캐주얼 게임

### 게임 세계관
> ###### 지구를 좋아해 지구에 내려온 별, 이 게임의 주인공 별땅이의 정체입니다. 원래 별은 먹지도, 자지도 않지만 별이 지구로 내려와 지구인들과 같이 생활하면 사람처럼 먹고, 자고, 행동하게 됩니다. 별땅이는 지구에 내려와 처음 음식을 먹어보고 반해버리고, 자신이 직접 요리를 배우고 음식을 만들기로 합니다.

### 캐릭터
> ![별땅이](https://user-images.githubusercontent.com/75113789/101164123-0868e680-3678-11eb-8e91-a587d40d0d83.png)
> ###### 하늘에서 사는 별이었지만, 고소공포증으로 인해 하늘의 친구의 도움을 받아 지구로 내려왔고 지구에서 난생 처음으로 음식을 먹어보고 반해서 식당을 운영하면서 맛있는 음식에 대해 배우기로 마음먹습니다. 살짝 맹한 구석이 있지만, 다른 존재(?)와 이야기하고 친해지는 것을 좋아해서 누구에게나 잘 다가갑니다.

### 게임 메커니즘
> ###### 별땅이가 식당을 운영하면서 요리를 하고, 손님들과 대화를 하며 메인 퀘스트를 완료해 스토리의 엔딩을 보면 게임의 진행이 끝남
> ###### 서브 퀘스트와 미니게임을 통해 요리 재료를 얻거나, 메인 퀘스트를 깨는데 도움을 얻을 수 있음


-------
## 작업 설명
#
![스크립트 구조](https://user-images.githubusercontent.com/75113789/101277327-fe212680-37f6-11eb-9452-2393ae793a78.PNG)
> ###### 씬은 크게 3개(Village, Minigame, Restaurant)씬으로 구성이 되어있으며, Database와 Inventory는 씬을 바꿔도 스크립트가 붙은 오브젝트가 없어지지 않게 하기위해 싱글톤 패턴을 사용하여 항상 오브젝트가 남아있게끔 함

#
![01_parkjunseo](https://user-images.githubusercontent.com/75113789/101164574-8d540000-3678-11eb-9453-d4d1fe8fcaae.png)
![02_parkjunseo](https://user-images.githubusercontent.com/75113789/101164577-8f1dc380-3678-11eb-808c-a94147d31d51.PNG)
![03_parkjunseo](https://user-images.githubusercontent.com/75113789/101164579-8f1dc380-3678-11eb-84bd-ec63756524e0.PNG)
![04_parkjunseo](https://user-images.githubusercontent.com/75113789/101164580-8fb65a00-3678-11eb-94d6-2fe6bb56bf10.PNG)
> ###### Village씬 화면들

#
![05_parkjunseo](https://user-images.githubusercontent.com/75113789/101164581-904ef080-3678-11eb-84e9-f408485d2e42.PNG)
![restaurant](https://user-images.githubusercontent.com/75113789/101278207-4fccaf80-37fd-11eb-8221-8c2fffeff745.PNG)
![restaurant2](https://user-images.githubusercontent.com/75113789/101278208-50fddc80-37fd-11eb-953c-0e110e278487.PNG)
> ###### Restaurant씬 화면들

#
![06_parkjunseo](https://user-images.githubusercontent.com/75113789/101164583-904ef080-3678-11eb-866c-cb89a1c44b36.PNG)
> ###### Minigame1
![minigame2](https://user-images.githubusercontent.com/75113789/101278334-f9ac3c00-37fd-11eb-9043-bdfa6b282069.PNG)
> ###### Minigame2
![minigame3](https://user-images.githubusercontent.com/75113789/101278336-fadd6900-37fd-11eb-8cf8-23d77db0ccad.PNG)
> ###### Minigame3
![minigame4](https://user-images.githubusercontent.com/75113789/101278337-fb75ff80-37fd-11eb-9090-03eed7db4282.PNG)
> ###### Minigame4
![minigame5](https://user-images.githubusercontent.com/75113789/101278338-fc0e9600-37fd-11eb-9b2b-2134780ae99e.PNG)
> ###### Minigame5






