import os
import sys
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.action_chains import ActionChains
import time
from selenium.webdriver.common.keys import Keys

def get_file_path(filename):
    """获取文件路径，文件需位于 .exe 所在目录"""
    if getattr(sys, 'frozen', False):
        # 获取打包后 .exe 所在的目录
        base_path = os.path.dirname(sys.executable)
    else:
        # 脚本运行时的目录
        base_path = os.path.dirname(os.path.abspath(__file__))
    return os.path.join(base_path, filename)

# 获取文件路径
lyrics_file_path = get_file_path("lyricsreturn.txt")
otto_file_path = get_file_path("otto.txt")

# 打印当前脚本文件或可执行文件的路径
print(f"Current script/executable path: {os.path.abspath(__file__)}")
print(f"Current directory: {os.path.dirname(os.path.abspath(__file__))}")

# 检查并创建默认文件
for file_path in [lyrics_file_path, otto_file_path]:
    if not os.path.exists(file_path):
        print(f"文件 {file_path} 不存在，正在创建默认文件...")
        with open(file_path, "w", encoding="utf-8") as f:
            f.write("这是默认内容，可以编辑此文件。\n")
    else:
        print(f"找到文件 {file_path}，内容如下：")
        try:
            with open(file_path, "r", encoding="utf-8") as f:
                print(f.read())
        except Exception as e:
            print(f"无法读取文件 {file_path}：{e}")


# 设置Chrome选项
options = webdriver.ChromeOptions()
options.add_argument('--disable-blink-features=AutomationControlled')
#options.add_argument('--headless')  # 如果你不需要看到浏览器界面，可以启用无头模式
options.add_argument('--disable-gpu')
options.add_argument('--no-sandbox')
options.add_argument('--disable-dev-shm-usage')

# 启动Chrome浏览器
driver = webdriver.Chrome(options=options)

try:
    # 打开目标网页
    driver.get('https://sonauto.ai/create')

    # 等待页面加载并找到“Continue”按钮
    wait = WebDriverWait(driver, 10)
    generate_button = wait.until(EC.presence_of_element_located((By.XPATH, '//button[contains(@class, "flex text-white items-center justify-center bg-primary p-2 sm:p-4 font-semibold border border-l-0 rounded-r-md text-sm h-8 sm:text-lg sm:h-12")]')))

    # 点击“Generate”按钮
    generate_button.click()

    # 等待页面加载并找到“Continue with Email”按钮
    continue_button = wait.until(EC.presence_of_element_located((By.XPATH, '//button[contains(@class, "inline-flex items-center justify-center whitespace-nowrap rounded-md text-sm font-medium ring-offset-background transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50 bg-primary text-primary-foreground hover:bg-primary/90 h-10 px-4 py-2 w-full mb-2")]')))

    # 点击“Continue with Email”按钮
    continue_button.click()
    time.sleep(5)

    # 找到电子邮件输入框并输入电子邮件地址
    email_input = wait.until(EC.presence_of_element_located((By.XPATH, '//input[@id="email"]')))
    email_input.send_keys('personal email removed for anonymity')

    # 找到密码输入框并输入密码
    password_input = wait.until(EC.presence_of_element_located((By.XPATH, '//input[@id="password"]')))
    password_input.send_keys('personal password removed for anonymity')

    # 点击“Sign In”按钮
    sign_in_button = wait.until(EC.presence_of_element_located((By.XPATH, '//button[contains(@class, "inline-flex items-center justify-center whitespace-nowrap rounded-md text-sm font-medium ring-offset-background transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50 bg-primary text-primary-foreground hover:bg-primary/90 h-10 px-4 py-2") and @type="submit"]')))
    sign_in_button.click()


    # 再次等待页面加载并找到“Generate”按钮
    generate_button = wait.until(EC.presence_of_element_located((By.XPATH, '//button[contains(@class, "flex text-white items-center justify-center bg-primary p-2 sm:p-4 font-semibold border border-l-0 rounded-r-md text-sm h-8 sm:text-lg sm:h-12")]')))

    # 输入自定义字符串
    custom_string = "根据以下关键词，提炼乐器、情绪、元素、曲风要素完成英文歌曲，注意歌词中不要包含乐器和曲风元素。"  # 你可以在编译器内自己编辑这个字符串

    # 从固定路径读取内容
    #otto_file_path = os.path.join(current_dir, 'otto.txt')  # 使用固定路径引用文件
    with open(otto_file_path, 'r', encoding='utf-8') as file:
        file_content = file.read()
    

    # 将自定义字符串和文件内容一起输入
    combined_input = custom_string + file_content

    # 使用你提供的 XPath 表达式
    input_element = wait.until(EC.presence_of_element_located((By.XPATH, '//input[@placeholder="A rock song about turtles flying" and @class="w-full pl-8 pr-10 sm:pl-10 sm:pr-12 border border-r-0 rounded-l-md outline-none p-2 text-sm h-8 sm:p-4 sm:text-lg sm:h-12"]')))

    # 点击输入框
    input_element.click()

    # 清空输入框内容
    input_element.clear()

    # 输入自定义内容
    input_element.send_keys(combined_input)

    # 等待阻挡元素消失
    wait.until(EC.invisibility_of_element_located((By.XPATH, '//div[@data-state="open" and @class="fixed inset-0 z-50 bg-black/80"]')))

    # 再次点击“Generate”按钮
    generate_button.click()

    time.sleep(70)

    extend_edit_button = wait.until(EC.presence_of_element_located((By.XPATH, '//button[contains(@class, "text-white px-4") and contains(text(), "Extend & Edit")]')))
    extend_edit_button.click()
    
    time.sleep(25)

    # 点击“Inpaint”按钮
    inpaint_button = wait.until(EC.presence_of_element_located((By.XPATH, '//button[@type="button" and @role="tab" and contains(text(), "Inpaint")]')))
    inpaint_button.click()
    print("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb")

    time.sleep(2)

    # 找到包含歌词的元素并点击
    lyrics_element = wait.until(EC.presence_of_element_located((By.XPATH, '//div[@data-slate-node="element"]')))
    lyrics_element.click()

    # 全选歌词文本
    actions = ActionChains(driver)
    actions.key_down(Keys.CONTROL).send_keys('a').key_up(Keys.CONTROL).perform()

    # 提取歌词文本
    lyrics_text = lyrics_element.text

    # 将歌词文本写入文件
    #lyrics_file_path = os.path.join(current_dir, 'lyricsreturn.txt')
    with open(lyrics_file_path, 'w', encoding='utf-8-sig') as lyrics_file:
        lyrics_file.write(lyrics_text)

    time.sleep(3)
    driver.back()
    time.sleep(5)
    print("aaaaaaaabbbbbbbbbbbbbcccccccccccccccccdddddddddddddddddddddddddddd")

    #button_selector = 'body > div.flex.flex-col.h-screen.bg-background.relative > div.flex.p-0.sm\\:p-2.flex-grow.overflow-auto > div.flex.flex-1.flex-col.relative.overflow-hidden > main > div > div.flex.max-w-\\[100\\%\\].overflow-x-scroll.snap-x.snap-start.snap-mandatory.px-4.pt-4.justify-start > div:nth-child(1) > div > div.bg-black.p-4.-mt-4 > div.flex.justify-between.items-center > button.bg-white.rounded-full.p-2'
    #button_element = wait.until(EC.presence_of_element_located((By.CSS_SELECTOR, button_selector)))
    #driver.execute_script("arguments[0].click();", button_element)

    button_selector = 'button.bg-white.rounded-full.p-2'
    button_element = wait.until(EC.presence_of_element_located((By.CSS_SELECTOR, button_selector)))
    driver.execute_script("arguments[0].click();", button_element)



    # 等待操作完成
    time.sleep(300)
finally:
    print("aaaaaaaaaaaaaaaaaaaaa")
    #driver.quit()