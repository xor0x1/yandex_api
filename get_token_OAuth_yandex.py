#Скрипт для получения токена OAuth gперед получением token нужно получить код подтверждения
#используя ссылку https://oauth.yandex.ru/authorize?response_type=code&client_id=<client id получим при создание приложения>
#действия кода подтверждения 10 минут.
import requests

#Укаажите client_id
client_id = ""

#Укаажите client_secret
client_secret = ""
code = ""

url = "https://oauth.yandex.ru/token"
data = {
    "grant_type": "authorization_code",
    "code": code,
    "client_id": client_id,
    "client_secret": client_secret
}

response = requests.post(url, data=data)
access_token = response.json()["access_token"]
print(access_token)
