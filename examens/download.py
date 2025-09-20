import os
import requests
import threading

counter = 0

def downloadSubject(code):
        global counter
        for any in range(24, 25):
            for mes in ["j", "s"]:
                for doc in ["l", "p"]:
                    url = f"https://www.selecat.cat/pau/pau_{code}{any:2d}{mes}{doc}.pdf".replace(" ", "0")
                    counter += 1
                    print(counter, "out of 2552")
                    r = requests.get(url, allow_redirects=True)
                    open(f'pau_{code}{any:2d}{mes}{doc}.pdf'.replace(" ", "0"), 'wb').write(r.content)
try:
    t = None
    for code in ["llca", "lles", "hist", "hfil", "angl", "fran", "alem", "ital", "biol","dibu","cite","elec","fisi","mate","tecn","quim","macs","ecem","geog","hart","llat","grec","lica","lies","fart","amus","diss","cuau","diar"]:
        t = threading.Thread(target=downloadSubject, args=(code,))
        t.start()
        t.join()
    os.system("pause")
except Exception as e:
    print(e)
    os.system("pause")
