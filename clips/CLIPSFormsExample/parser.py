import random

def generate_clips_code(description_file, rules_file, output_file):
    # Чтение описаний фактов
    facts_description = {}
    with open(description_file, 'r', encoding='utf-8') as file:
        for line in file:
            if line.strip():
                parts = line.strip().split(' ', 1)
                if len(parts) == 2:
                    fact_id, description = parts
                    facts_description[fact_id] = description

    # Чтение правил
    rules = []
    with open(rules_file, 'r', encoding='utf-8') as file:
        for line in file:
            if line.strip():
                rules.append(line.strip())

    # Генерация CLIPS-кода
    clips_code = """
(deftemplate ioproxy  ; шаблон факта-посредника для обмена информацией с GUI
    (slot fact-id)        ; теоретически тут id факта для изменения
    (multislot answers)   ; возможные ответы
    (multislot messages)  ; исходящие сообщения
    (slot reaction)       ; возможные ответы пользователя
    (slot value)          ; выбор пользователя
    (slot restore)        ; забыл зачем это поле
)

; Собственно экземпляр факта ioproxy
(deffacts proxy-fact
    (ioproxy
        (fact-id 0112) ; это поле пока что не задействовано
        (value none)   ; значение пустое
        (messages)     ; мультислот messages изначально пуст
    )
)

(defrule clear-messages
    (declare (salience 90))
    ?clear-msg-flg <- (clearmessage)
    ?proxy <- (ioproxy)
    =>
    (modify ?proxy (messages))
    (retract ?clear-msg-flg)
    (printout t "Messages cleared ..." crlf)	
)

(defrule addmessage
    (declare (salience 100))
    ?currmessages <- (message $?messagelist)
    ?fact <- (ioproxy (messages $?ioproxymessages))
    =>
    (modify ?fact (messages $?ioproxymessages $?messagelist))
    (retract ?currmessages)
    (halt)
)

(deftemplate theorem
    (slot name)
    (slot coef (type FLOAT) (default 1.0))
)

(defrule combine
    (declare (salience 90))
    ?i1 <- (theorem (name ?f1) (coef ?coef1))
    ?i2 <- (theorem (name ?f2) (coef ?coef2))
    =>
    (if (and (eq ?f1 ?f2) (!= ?coef1 ?coef2)) then
        (assert (theorem (name ?f1) (coef (- (+ ?coef1 ?coef2) (* ?coef1 ?coef2)))))
        (retract ?i1)
        (retract ?i2)
        (assert (message (str-cat ?f1 " " (- (+ ?coef1 ?coef2) (* ?coef1 ?coef2)) " : Коэффициент пересчитан")))
    )
)
"""

    # Генерация правил
    rule_counter = 1
    for rule in rules:
        if "=>" in rule:
            inputs, output = rule.split("=>")
            inputs = inputs.strip().split("&")
            output = output.strip()

            # Формирование условия
            conditions = []
            for i, input_fact in enumerate(inputs):
                conditions.append(f"(theorem (name {input_fact.strip()}) (coef ?c{i+1}))")

            # Формирование действия
            coef = round(random.uniform(0.1, 0.9), 3)
            min_coefs = " ".join([f"?c{i+1}" for i in range(len(inputs))])
            actions = f"""
    (assert (theorem (name {output}) (coef (* {coef} (min {min_coefs})))))
    (assert (message (str-cat "{rule} : " (* {coef} (min {min_coefs})) " ({coef} * (min {min_coefs}))")))
"""

            # Создание правила
            clips_code += f"""
(defrule rule{rule_counter}
    (declare (salience 50))
    {' '.join(conditions)}
    =>
{actions}
)
"""
            rule_counter += 1

    # Запись результата в файл
    with open(output_file, 'w', encoding='utf-8') as file:
        file.write(clips_code)

# Пример использования
generate_clips_code("description.txt", "rules.txt", "generated_rules.clp")