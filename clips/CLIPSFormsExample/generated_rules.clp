
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
        (assert (message (str-cat ?f1 " " (- (+ ?coef1 ?coef2) (* ?coef1 ?coef2)) " : Коэффициент пересчитали")))
    )
)

(defrule rule1
    (declare (salience 50))
    (theorem (name A1) (coef ?c1))
    =>

    (assert (theorem (name T1) (coef (* 0.818 (min ?c1)))))
    (assert (message (str-cat "A1=>T1 : " (* 0.818 (min ?c1)) " (0.818 * (min ?c1))")))

)

(defrule rule2
    (declare (salience 50))
    (theorem (name A1) (coef ?c1))
    =>

    (assert (theorem (name T2) (coef (* 0.768 (min ?c1)))))
    (assert (message (str-cat "A1=>T2 : " (* 0.768 (min ?c1)) " (0.768 * (min ?c1))")))

)

(defrule rule3
    (declare (salience 50))
    (theorem (name A1) (coef ?c1)) (theorem (name A3) (coef ?c2))
    =>

    (assert (theorem (name T3) (coef (* 0.182 (min ?c1 ?c2)))))
    (assert (message (str-cat "A1&A3=>T3 : " (* 0.182 (min ?c1 ?c2)) " (0.182 * (min ?c1 ?c2))")))

)

(defrule rule4
    (declare (salience 50))
    (theorem (name A1) (coef ?c1)) (theorem (name T1) (coef ?c2))
    =>

    (assert (theorem (name T4) (coef (* 0.732 (min ?c1 ?c2)))))
    (assert (message (str-cat "A1&T1=>T4 : " (* 0.732 (min ?c1 ?c2)) " (0.732 * (min ?c1 ?c2))")))

)

(defrule rule5
    (declare (salience 50))
    (theorem (name T1) (coef ?c1)) (theorem (name T2) (coef ?c2))
    =>

    (assert (theorem (name T5) (coef (* 0.564 (min ?c1 ?c2)))))
    (assert (message (str-cat "T1&T2=>T5 : " (* 0.564 (min ?c1 ?c2)) " (0.564 * (min ?c1 ?c2))")))

)

(defrule rule6
    (declare (salience 50))
    (theorem (name A13) (coef ?c1)) (theorem (name A14) (coef ?c2))
    =>

    (assert (theorem (name T6) (coef (* 0.259 (min ?c1 ?c2)))))
    (assert (message (str-cat "A13&A14=>T6 : " (* 0.259 (min ?c1 ?c2)) " (0.259 * (min ?c1 ?c2))")))

)

(defrule rule7
    (declare (salience 50))
    (theorem (name T4) (coef ?c1)) (theorem (name T6) (coef ?c2))
    =>

    (assert (theorem (name T7) (coef (* 0.36 (min ?c1 ?c2)))))
    (assert (message (str-cat "T4&T6=>T7 : " (* 0.36 (min ?c1 ?c2)) " (0.36 * (min ?c1 ?c2))")))

)

(defrule rule8
    (declare (salience 50))
    (theorem (name T2) (coef ?c1))
    =>

    (assert (theorem (name T8) (coef (* 0.652 (min ?c1)))))
    (assert (message (str-cat "T2=>T8 : " (* 0.652 (min ?c1)) " (0.652 * (min ?c1))")))

)

(defrule rule9
    (declare (salience 50))
    (theorem (name T2) (coef ?c1))
    =>

    (assert (theorem (name T9) (coef (* 0.109 (min ?c1)))))
    (assert (message (str-cat "T2=>T9 : " (* 0.109 (min ?c1)) " (0.109 * (min ?c1))")))

)

(defrule rule10
    (declare (salience 50))
    (theorem (name T4) (coef ?c1)) (theorem (name T7) (coef ?c2))
    =>

    (assert (theorem (name T10) (coef (* 0.252 (min ?c1 ?c2)))))
    (assert (message (str-cat "T4&T7=>T10 : " (* 0.252 (min ?c1 ?c2)) " (0.252 * (min ?c1 ?c2))")))

)

(defrule rule11
    (declare (salience 50))
    (theorem (name T3) (coef ?c1)) (theorem (name T7) (coef ?c2))
    =>

    (assert (theorem (name T11) (coef (* 0.43 (min ?c1 ?c2)))))
    (assert (message (str-cat "T3&T7=>T11 : " (* 0.43 (min ?c1 ?c2)) " (0.43 * (min ?c1 ?c2))")))

)

(defrule rule12
    (declare (salience 50))
    (theorem (name A10) (coef ?c1)) (theorem (name T11) (coef ?c2))
    =>

    (assert (theorem (name T12) (coef (* 0.43 (min ?c1 ?c2)))))
    (assert (message (str-cat "A10&T11=>T12 : " (* 0.43 (min ?c1 ?c2)) " (0.43 * (min ?c1 ?c2))")))

)

(defrule rule13
    (declare (salience 50))
    (theorem (name T5) (coef ?c1))
    =>

    (assert (theorem (name T13) (coef (* 0.462 (min ?c1)))))
    (assert (message (str-cat "T5=>T13 : " (* 0.462 (min ?c1)) " (0.462 * (min ?c1))")))

)

(defrule rule14
    (declare (salience 50))
    (theorem (name A12) (coef ?c1)) (theorem (name T13) (coef ?c2))
    =>

    (assert (theorem (name T14) (coef (* 0.22 (min ?c1 ?c2)))))
    (assert (message (str-cat "A12&T13=>T14 : " (* 0.22 (min ?c1 ?c2)) " (0.22 * (min ?c1 ?c2))")))

)

(defrule rule15
    (declare (salience 50))
    (theorem (name A12) (coef ?c1)) (theorem (name T13) (coef ?c2))
    =>

    (assert (theorem (name T15) (coef (* 0.68 (min ?c1 ?c2)))))
    (assert (message (str-cat "A12&T13=>T15 : " (* 0.68 (min ?c1 ?c2)) " (0.68 * (min ?c1 ?c2))")))

)

(defrule rule16
    (declare (salience 50))
    (theorem (name T7) (coef ?c1)) (theorem (name T10) (coef ?c2))
    =>

    (assert (theorem (name T16) (coef (* 0.234 (min ?c1 ?c2)))))
    (assert (message (str-cat "T7&T10=>T16 : " (* 0.234 (min ?c1 ?c2)) " (0.234 * (min ?c1 ?c2))")))

)

(defrule rule17
    (declare (salience 50))
    (theorem (name T4) (coef ?c1)) (theorem (name T6) (coef ?c2))
    =>

    (assert (theorem (name T17) (coef (* 0.76 (min ?c1 ?c2)))))
    (assert (message (str-cat "T4&T6=>T17 : " (* 0.76 (min ?c1 ?c2)) " (0.76 * (min ?c1 ?c2))")))

)

(defrule rule18
    (declare (salience 50))
    (theorem (name T6) (coef ?c1)) (theorem (name T10) (coef ?c2))
    =>

    (assert (theorem (name T18) (coef (* 0.21 (min ?c1 ?c2)))))
    (assert (message (str-cat "T6&T10=>T18 : " (* 0.21 (min ?c1 ?c2)) " (0.21 * (min ?c1 ?c2))")))

)

(defrule rule19
    (declare (salience 50))
    (theorem (name T2) (coef ?c1)) (theorem (name T5) (coef ?c2))
    =>

    (assert (theorem (name T19) (coef (* 0.162 (min ?c1 ?c2)))))
    (assert (message (str-cat "T2&T5=>T19 : " (* 0.162 (min ?c1 ?c2)) " (0.162 * (min ?c1 ?c2))")))

)

(defrule rule20
    (declare (salience 50))
    (theorem (name T7) (coef ?c1)) (theorem (name T16) (coef ?c2))
    =>

    (assert (theorem (name T20) (coef (* 0.891 (min ?c1 ?c2)))))
    (assert (message (str-cat "T7&T16=>T20 : " (* 0.891 (min ?c1 ?c2)) " (0.891 * (min ?c1 ?c2))")))

)

(defrule rule21
    (declare (salience 50))
    (theorem (name T6) (coef ?c1)) (theorem (name T11) (coef ?c2))
    =>

    (assert (theorem (name T21) (coef (* 0.105 (min ?c1 ?c2)))))
    (assert (message (str-cat "T6&T11=>T21 : " (* 0.105 (min ?c1 ?c2)) " (0.105 * (min ?c1 ?c2))")))

)

(defrule rule22
    (declare (salience 50))
    (theorem (name T6) (coef ?c1)) (theorem (name T13) (coef ?c2))
    =>

    (assert (theorem (name T22) (coef (* 0.165 (min ?c1 ?c2)))))
    (assert (message (str-cat "T6&T13=>T22 : " (* 0.165 (min ?c1 ?c2)) " (0.165 * (min ?c1 ?c2))")))

)

(defrule rule23
    (declare (salience 50))
    (theorem (name T10) (coef ?c1)) (theorem (name T14) (coef ?c2))
    =>

    (assert (theorem (name T23) (coef (* 0.295 (min ?c1 ?c2)))))
    (assert (message (str-cat "T10&T14=>T23 : " (* 0.295 (min ?c1 ?c2)) " (0.295 * (min ?c1 ?c2))")))

)

(defrule rule24
    (declare (salience 50))
    (theorem (name T11) (coef ?c1)) (theorem (name T13) (coef ?c2))
    =>

    (assert (theorem (name T24) (coef (* 0.719 (min ?c1 ?c2)))))
    (assert (message (str-cat "T11&T13=>T24 : " (* 0.719 (min ?c1 ?c2)) " (0.719 * (min ?c1 ?c2))")))

)

(defrule rule25
    (declare (salience 50))
    (theorem (name T11) (coef ?c1)) (theorem (name T12) (coef ?c2))
    =>

    (assert (theorem (name T25) (coef (* 0.751 (min ?c1 ?c2)))))
    (assert (message (str-cat "T11&T12=>T25 : " (* 0.751 (min ?c1 ?c2)) " (0.751 * (min ?c1 ?c2))")))

)

(defrule rule26
    (declare (salience 50))
    (theorem (name T5) (coef ?c1)) (theorem (name T23) (coef ?c2))
    =>

    (assert (theorem (name T26) (coef (* 0.552 (min ?c1 ?c2)))))
    (assert (message (str-cat "T5&T23=>T26 : " (* 0.552 (min ?c1 ?c2)) " (0.552 * (min ?c1 ?c2))")))

)

(defrule rule27
    (declare (salience 50))
    (theorem (name T26) (coef ?c1))
    =>

    (assert (theorem (name T27) (coef (* 0.516 (min ?c1)))))
    (assert (message (str-cat "T26=>T27 : " (* 0.516 (min ?c1)) " (0.516 * (min ?c1))")))

)

(defrule rule28
    (declare (salience 50))
    (theorem (name T26) (coef ?c1))
    =>

    (assert (theorem (name T28) (coef (* 0.596 (min ?c1)))))
    (assert (message (str-cat "T26=>T28 : " (* 0.596 (min ?c1)) " (0.596 * (min ?c1))")))

)

(defrule rule29
    (declare (salience 50))
    (theorem (name T3) (coef ?c1)) (theorem (name T5) (coef ?c2))
    =>

    (assert (theorem (name T29) (coef (* 0.712 (min ?c1 ?c2)))))
    (assert (message (str-cat "T3&T5=>T29 : " (* 0.712 (min ?c1 ?c2)) " (0.712 * (min ?c1 ?c2))")))

)

(defrule rule30
    (declare (salience 50))
    (theorem (name T17) (coef ?c1)) (theorem (name T4) (coef ?c2))
    =>

    (assert (theorem (name T30) (coef (* 0.685 (min ?c1 ?c2)))))
    (assert (message (str-cat "T17&T4=>T30 : " (* 0.685 (min ?c1 ?c2)) " (0.685 * (min ?c1 ?c2))")))

)

(defrule rule31
    (declare (salience 50))
    (theorem (name T8) (coef ?c1)) (theorem (name T19) (coef ?c2))
    =>

    (assert (theorem (name T31) (coef (* 0.64 (min ?c1 ?c2)))))
    (assert (message (str-cat "T8&T19=>T31 : " (* 0.64 (min ?c1 ?c2)) " (0.64 * (min ?c1 ?c2))")))

)

(defrule rule32
    (declare (salience 50))
    (theorem (name T3) (coef ?c1)) (theorem (name T8) (coef ?c2))
    =>

    (assert (theorem (name T32) (coef (* 0.845 (min ?c1 ?c2)))))
    (assert (message (str-cat "T3&T8=>T32 : " (* 0.845 (min ?c1 ?c2)) " (0.845 * (min ?c1 ?c2))")))

)

(defrule rule33
    (declare (salience 50))
    (theorem (name T11) (coef ?c1)) (theorem (name T32) (coef ?c2))
    =>

    (assert (theorem (name T33) (coef (* 0.747 (min ?c1 ?c2)))))
    (assert (message (str-cat "T11&T32=>T33 : " (* 0.747 (min ?c1 ?c2)) " (0.747 * (min ?c1 ?c2))")))

)

(defrule rule34
    (declare (salience 50))
    (theorem (name T23) (coef ?c1)) (theorem (name T31) (coef ?c2))
    =>

    (assert (theorem (name T34) (coef (* 0.525 (min ?c1 ?c2)))))
    (assert (message (str-cat "T23&T31=>T34 : " (* 0.525 (min ?c1 ?c2)) " (0.525 * (min ?c1 ?c2))")))

)

(defrule rule35
    (declare (salience 50))
    (theorem (name T23) (coef ?c1)) (theorem (name T34) (coef ?c2))
    =>

    (assert (theorem (name T35) (coef (* 0.615 (min ?c1 ?c2)))))
    (assert (message (str-cat "T23&T34=>T35 : " (* 0.615 (min ?c1 ?c2)) " (0.615 * (min ?c1 ?c2))")))

)

(defrule rule36
    (declare (salience 50))
    (theorem (name T11) (coef ?c1)) (theorem (name T25) (coef ?c2))
    =>

    (assert (theorem (name T36) (coef (* 0.453 (min ?c1 ?c2)))))
    (assert (message (str-cat "T11&T25=>T36 : " (* 0.453 (min ?c1 ?c2)) " (0.453 * (min ?c1 ?c2))")))

)

(defrule rule37
    (declare (salience 50))
    (theorem (name T36) (coef ?c1))
    =>

    (assert (theorem (name T37) (coef (* 0.7 (min ?c1)))))
    (assert (message (str-cat "T36=>T37 : " (* 0.7 (min ?c1)) " (0.7 * (min ?c1))")))

)

(defrule rule38
    (declare (salience 50))
    (theorem (name T32) (coef ?c1)) (theorem (name T33) (coef ?c2))
    =>

    (assert (theorem (name T38) (coef (* 0.589 (min ?c1 ?c2)))))
    (assert (message (str-cat "T32&T33=>T38 : " (* 0.589 (min ?c1 ?c2)) " (0.589 * (min ?c1 ?c2))")))

)

(defrule rule39
    (declare (salience 50))
    (theorem (name T11) (coef ?c1)) (theorem (name T22) (coef ?c2))
    =>

    (assert (theorem (name T39) (coef (* 0.679 (min ?c1 ?c2)))))
    (assert (message (str-cat "T11&T22=>T39 : " (* 0.679 (min ?c1 ?c2)) " (0.679 * (min ?c1 ?c2))")))

)

(defrule rule40
    (declare (salience 50))
    (theorem (name T11) (coef ?c1)) (theorem (name T33) (coef ?c2))
    =>

    (assert (theorem (name T40) (coef (* 0.361 (min ?c1 ?c2)))))
    (assert (message (str-cat "T11&T33=>T40 : " (* 0.361 (min ?c1 ?c2)) " (0.361 * (min ?c1 ?c2))")))

)

(defrule rule41
    (declare (salience 50))
    (theorem (name T20) (coef ?c1)) (theorem (name T21) (coef ?c2))
    =>

    (assert (theorem (name T41) (coef (* 0.389 (min ?c1 ?c2)))))
    (assert (message (str-cat "T20&T21=>T41 : " (* 0.389 (min ?c1 ?c2)) " (0.389 * (min ?c1 ?c2))")))

)

(defrule rule42
    (declare (salience 50))
    (theorem (name T7) (coef ?c1)) (theorem (name T41) (coef ?c2))
    =>

    (assert (theorem (name T42) (coef (* 0.748 (min ?c1 ?c2)))))
    (assert (message (str-cat "T7&T41=>T42 : " (* 0.748 (min ?c1 ?c2)) " (0.748 * (min ?c1 ?c2))")))

)

(defrule rule43
    (declare (salience 50))
    (theorem (name T11) (coef ?c1)) (theorem (name T21) (coef ?c2))
    =>

    (assert (theorem (name T43) (coef (* 0.22 (min ?c1 ?c2)))))
    (assert (message (str-cat "T11&T21=>T43 : " (* 0.22 (min ?c1 ?c2)) " (0.22 * (min ?c1 ?c2))")))

)

(defrule rule44
    (declare (salience 50))
    (theorem (name T30) (coef ?c1)) (theorem (name T38) (coef ?c2))
    =>

    (assert (theorem (name T44) (coef (* 0.358 (min ?c1 ?c2)))))
    (assert (message (str-cat "T30&T38=>T44 : " (* 0.358 (min ?c1 ?c2)) " (0.358 * (min ?c1 ?c2))")))

)

(defrule rule45
    (declare (salience 50))
    (theorem (name T3) (coef ?c1)) (theorem (name T33) (coef ?c2))
    =>

    (assert (theorem (name T45) (coef (* 0.399 (min ?c1 ?c2)))))
    (assert (message (str-cat "T3&T33=>T45 : " (* 0.399 (min ?c1 ?c2)) " (0.399 * (min ?c1 ?c2))")))

)

(defrule rule46
    (declare (salience 50))
    (theorem (name T11) (coef ?c1)) (theorem (name T39) (coef ?c2))
    =>

    (assert (theorem (name T46) (coef (* 0.672 (min ?c1 ?c2)))))
    (assert (message (str-cat "T11&T39=>T46 : " (* 0.672 (min ?c1 ?c2)) " (0.672 * (min ?c1 ?c2))")))

)

(defrule rule47
    (declare (salience 50))
    (theorem (name T36) (coef ?c1)) (theorem (name T43) (coef ?c2))
    =>

    (assert (theorem (name T47) (coef (* 0.304 (min ?c1 ?c2)))))
    (assert (message (str-cat "T36&T43=>T47 : " (* 0.304 (min ?c1 ?c2)) " (0.304 * (min ?c1 ?c2))")))

)

(defrule rule48
    (declare (salience 50))
    (theorem (name T45) (coef ?c1)) (theorem (name T38) (coef ?c2))
    =>

    (assert (theorem (name T48) (coef (* 0.88 (min ?c1 ?c2)))))
    (assert (message (str-cat "T45&T38=>T48 : " (* 0.88 (min ?c1 ?c2)) " (0.88 * (min ?c1 ?c2))")))

)

(defrule rule49
    (declare (salience 50))
    (theorem (name T26) (coef ?c1)) (theorem (name T37) (coef ?c2))
    =>

    (assert (theorem (name T49) (coef (* 0.319 (min ?c1 ?c2)))))
    (assert (message (str-cat "T26&T37=>T49 : " (* 0.319 (min ?c1 ?c2)) " (0.319 * (min ?c1 ?c2))")))

)

(defrule rule50
    (declare (salience 50))
    (theorem (name T30) (coef ?c1)) (theorem (name T32) (coef ?c2))
    =>

    (assert (theorem (name T50) (coef (* 0.744 (min ?c1 ?c2)))))
    (assert (message (str-cat "T30&T32=>T50 : " (* 0.744 (min ?c1 ?c2)) " (0.744 * (min ?c1 ?c2))")))

)

(defrule rule51
    (declare (salience 50))
    (theorem (name T31) (coef ?c1)) (theorem (name T50) (coef ?c2))
    =>

    (assert (theorem (name T51) (coef (* 0.211 (min ?c1 ?c2)))))
    (assert (message (str-cat "T31&T50=>T51 : " (* 0.211 (min ?c1 ?c2)) " (0.211 * (min ?c1 ?c2))")))

)

(defrule rule52
    (declare (salience 50))
    (theorem (name T22) (coef ?c1)) (theorem (name T24) (coef ?c2))
    =>

    (assert (theorem (name T52) (coef (* 0.534 (min ?c1 ?c2)))))
    (assert (message (str-cat "T22&T24=>T52 : " (* 0.534 (min ?c1 ?c2)) " (0.534 * (min ?c1 ?c2))")))

)

(defrule rule53
    (declare (salience 50))
    (theorem (name T14) (coef ?c1)) (theorem (name T39) (coef ?c2))
    =>

    (assert (theorem (name T53) (coef (* 0.654 (min ?c1 ?c2)))))
    (assert (message (str-cat "T14&T39=>T53 : " (* 0.654 (min ?c1 ?c2)) " (0.654 * (min ?c1 ?c2))")))

)

(defrule rule54
    (declare (salience 50))
    (theorem (name T20) (coef ?c1)) (theorem (name T46) (coef ?c2))
    =>

    (assert (theorem (name T54) (coef (* 0.596 (min ?c1 ?c2)))))
    (assert (message (str-cat "T20&T46=>T54 : " (* 0.596 (min ?c1 ?c2)) " (0.596 * (min ?c1 ?c2))")))

)

(defrule rule55
    (declare (salience 50))
    (theorem (name T43) (coef ?c1)) (theorem (name T45) (coef ?c2))
    =>

    (assert (theorem (name T55) (coef (* 0.548 (min ?c1 ?c2)))))
    (assert (message (str-cat "T43&T45=>T55 : " (* 0.548 (min ?c1 ?c2)) " (0.548 * (min ?c1 ?c2))")))

)

(defrule rule56
    (declare (salience 50))
    (theorem (name T8) (coef ?c1)) (theorem (name T19) (coef ?c2))
    =>

    (assert (theorem (name T56) (coef (* 0.864 (min ?c1 ?c2)))))
    (assert (message (str-cat "T8&T19=>T56 : " (* 0.864 (min ?c1 ?c2)) " (0.864 * (min ?c1 ?c2))")))

)

(defrule rule57
    (declare (salience 50))
    (theorem (name T48) (coef ?c1)) (theorem (name T30) (coef ?c2))
    =>

    (assert (theorem (name T57) (coef (* 0.779 (min ?c1 ?c2)))))
    (assert (message (str-cat "T48&T30=>T57 : " (* 0.779 (min ?c1 ?c2)) " (0.779 * (min ?c1 ?c2))")))

)

(defrule rule58
    (declare (salience 50))
    (theorem (name T20) (coef ?c1)) (theorem (name T52) (coef ?c2))
    =>

    (assert (theorem (name T58) (coef (* 0.491 (min ?c1 ?c2)))))
    (assert (message (str-cat "T20&T52=>T58 : " (* 0.491 (min ?c1 ?c2)) " (0.491 * (min ?c1 ?c2))")))

)

(defrule rule59
    (declare (salience 50))
    (theorem (name T58) (coef ?c1)) (theorem (name T30) (coef ?c2))
    =>

    (assert (theorem (name T59) (coef (* 0.188 (min ?c1 ?c2)))))
    (assert (message (str-cat "T58&T30=>T59 : " (* 0.188 (min ?c1 ?c2)) " (0.188 * (min ?c1 ?c2))")))

)

(defrule rule60
    (declare (salience 50))
    (theorem (name T45) (coef ?c1)) (theorem (name T54) (coef ?c2))
    =>

    (assert (theorem (name T60) (coef (* 0.487 (min ?c1 ?c2)))))
    (assert (message (str-cat "T45&T54=>T60 : " (* 0.487 (min ?c1 ?c2)) " (0.487 * (min ?c1 ?c2))")))

)

(defrule rule61
    (declare (salience 50))
    (theorem (name T22) (coef ?c1)) (theorem (name T41) (coef ?c2))
    =>

    (assert (theorem (name T61) (coef (* 0.538 (min ?c1 ?c2)))))
    (assert (message (str-cat "T22&T41=>T61 : " (* 0.538 (min ?c1 ?c2)) " (0.538 * (min ?c1 ?c2))")))

)

(defrule rule62
    (declare (salience 50))
    (theorem (name T42) (coef ?c1)) (theorem (name T43) (coef ?c2))
    =>

    (assert (theorem (name T62) (coef (* 0.185 (min ?c1 ?c2)))))
    (assert (message (str-cat "T42&T43=>T62 : " (* 0.185 (min ?c1 ?c2)) " (0.185 * (min ?c1 ?c2))")))

)

(defrule rule63
    (declare (salience 50))
    (theorem (name T45) (coef ?c1)) (theorem (name T29) (coef ?c2))
    =>

    (assert (theorem (name T63) (coef (* 0.468 (min ?c1 ?c2)))))
    (assert (message (str-cat "T45&T29=>T63 : " (* 0.468 (min ?c1 ?c2)) " (0.468 * (min ?c1 ?c2))")))

)

(defrule rule64
    (declare (salience 50))
    (theorem (name T5) (coef ?c1)) (theorem (name T28) (coef ?c2))
    =>

    (assert (theorem (name T64) (coef (* 0.851 (min ?c1 ?c2)))))
    (assert (message (str-cat "T5&T28=>T64 : " (* 0.851 (min ?c1 ?c2)) " (0.851 * (min ?c1 ?c2))")))

)

(defrule rule65
    (declare (salience 50))
    (theorem (name T64) (coef ?c1)) (theorem (name T7) (coef ?c2))
    =>

    (assert (theorem (name T65) (coef (* 0.856 (min ?c1 ?c2)))))
    (assert (message (str-cat "T64&T7=>T65 : " (* 0.856 (min ?c1 ?c2)) " (0.856 * (min ?c1 ?c2))")))

)

(defrule rule66
    (declare (salience 50))
    (theorem (name T47) (coef ?c1)) (theorem (name T46) (coef ?c2))
    =>

    (assert (theorem (name T66) (coef (* 0.153 (min ?c1 ?c2)))))
    (assert (message (str-cat "T47&T46=>T66 : " (* 0.153 (min ?c1 ?c2)) " (0.153 * (min ?c1 ?c2))")))

)

(defrule rule67
    (declare (salience 50))
    (theorem (name T52) (coef ?c1)) (theorem (name T7) (coef ?c2))
    =>

    (assert (theorem (name T67) (coef (* 0.314 (min ?c1 ?c2)))))
    (assert (message (str-cat "T52&T7=>T67 : " (* 0.314 (min ?c1 ?c2)) " (0.314 * (min ?c1 ?c2))")))

)

(defrule rule68
    (declare (salience 50))
    (theorem (name T45) (coef ?c1)) (theorem (name T48) (coef ?c2))
    =>

    (assert (theorem (name T68) (coef (* 0.489 (min ?c1 ?c2)))))
    (assert (message (str-cat "T45&T48=>T68 : " (* 0.489 (min ?c1 ?c2)) " (0.489 * (min ?c1 ?c2))")))

)

(defrule rule69
    (declare (salience 50))
    (theorem (name T48) (coef ?c1)) (theorem (name T68) (coef ?c2))
    =>

    (assert (theorem (name T69) (coef (* 0.293 (min ?c1 ?c2)))))
    (assert (message (str-cat "T48&T68=>T69 : " (* 0.293 (min ?c1 ?c2)) " (0.293 * (min ?c1 ?c2))")))

)

(defrule rule70
    (declare (salience 50))
    (theorem (name T44) (coef ?c1)) (theorem (name T48) (coef ?c2))
    =>

    (assert (theorem (name T70) (coef (* 0.316 (min ?c1 ?c2)))))
    (assert (message (str-cat "T44&T48=>T70 : " (* 0.316 (min ?c1 ?c2)) " (0.316 * (min ?c1 ?c2))")))

)

(defrule rule71
    (declare (salience 50))
    (theorem (name T45) (coef ?c1)) (theorem (name T63) (coef ?c2))
    =>

    (assert (theorem (name T71) (coef (* 0.837 (min ?c1 ?c2)))))
    (assert (message (str-cat "T45&T63=>T71 : " (* 0.837 (min ?c1 ?c2)) " (0.837 * (min ?c1 ?c2))")))

)

(defrule rule72
    (declare (salience 50))
    (theorem (name T63) (coef ?c1)) (theorem (name T71) (coef ?c2))
    =>

    (assert (theorem (name T72) (coef (* 0.466 (min ?c1 ?c2)))))
    (assert (message (str-cat "T63&T71=>T72 : " (* 0.466 (min ?c1 ?c2)) " (0.466 * (min ?c1 ?c2))")))

)

(defrule rule73
    (declare (salience 50))
    (theorem (name T68) (coef ?c1)) (theorem (name T71) (coef ?c2))
    =>

    (assert (theorem (name T73) (coef (* 0.165 (min ?c1 ?c2)))))
    (assert (message (str-cat "T68&T71=>T73 : " (* 0.165 (min ?c1 ?c2)) " (0.165 * (min ?c1 ?c2))")))

)

(defrule rule74
    (declare (salience 50))
    (theorem (name T26) (coef ?c1)) (theorem (name T37) (coef ?c2))
    =>

    (assert (theorem (name T74) (coef (* 0.742 (min ?c1 ?c2)))))
    (assert (message (str-cat "T26&T37=>T74 : " (* 0.742 (min ?c1 ?c2)) " (0.742 * (min ?c1 ?c2))")))

)

(defrule rule75
    (declare (salience 50))
    (theorem (name T55) (coef ?c1)) (theorem (name T60) (coef ?c2))
    =>

    (assert (theorem (name T75) (coef (* 0.849 (min ?c1 ?c2)))))
    (assert (message (str-cat "T55&T60=>T75 : " (* 0.849 (min ?c1 ?c2)) " (0.849 * (min ?c1 ?c2))")))

)

(defrule rule76
    (declare (salience 50))
    (theorem (name T49) (coef ?c1)) (theorem (name T50) (coef ?c2))
    =>

    (assert (theorem (name T76) (coef (* 0.392 (min ?c1 ?c2)))))
    (assert (message (str-cat "T49&T50=>T76 : " (* 0.392 (min ?c1 ?c2)) " (0.392 * (min ?c1 ?c2))")))

)

(defrule rule77
    (declare (salience 50))
    (theorem (name T48) (coef ?c1)) (theorem (name T69) (coef ?c2))
    =>

    (assert (theorem (name T77) (coef (* 0.34 (min ?c1 ?c2)))))
    (assert (message (str-cat "T48&T69=>T77 : " (* 0.34 (min ?c1 ?c2)) " (0.34 * (min ?c1 ?c2))")))

)

(defrule rule78
    (declare (salience 50))
    (theorem (name T55) (coef ?c1)) (theorem (name T77) (coef ?c2))
    =>

    (assert (theorem (name T78) (coef (* 0.197 (min ?c1 ?c2)))))
    (assert (message (str-cat "T55&T77=>T78 : " (* 0.197 (min ?c1 ?c2)) " (0.197 * (min ?c1 ?c2))")))

)

(defrule rule79
    (declare (salience 50))
    (theorem (name T68) (coef ?c1)) (theorem (name T69) (coef ?c2))
    =>

    (assert (theorem (name T79) (coef (* 0.313 (min ?c1 ?c2)))))
    (assert (message (str-cat "T68&T69=>T79 : " (* 0.313 (min ?c1 ?c2)) " (0.313 * (min ?c1 ?c2))")))

)

(defrule rule80
    (declare (salience 50))
    (theorem (name T69) (coef ?c1)) (theorem (name T78) (coef ?c2))
    =>

    (assert (theorem (name T80) (coef (* 0.694 (min ?c1 ?c2)))))
    (assert (message (str-cat "T69&T78=>T80 : " (* 0.694 (min ?c1 ?c2)) " (0.694 * (min ?c1 ?c2))")))

)

(defrule rule81
    (declare (salience 50))
    (theorem (name T78) (coef ?c1)) (theorem (name T79) (coef ?c2))
    =>

    (assert (theorem (name T81) (coef (* 0.537 (min ?c1 ?c2)))))
    (assert (message (str-cat "T78&T79=>T81 : " (* 0.537 (min ?c1 ?c2)) " (0.537 * (min ?c1 ?c2))")))

)

(defrule rule82
    (declare (salience 50))
    (theorem (name T81) (coef ?c1)) (theorem (name T80) (coef ?c2))
    =>

    (assert (theorem (name T82) (coef (* 0.491 (min ?c1 ?c2)))))
    (assert (message (str-cat "T81&T80=>T82 : " (* 0.491 (min ?c1 ?c2)) " (0.491 * (min ?c1 ?c2))")))

)

(defrule rule83
    (declare (salience 50))
    (theorem (name T65) (coef ?c1)) (theorem (name T80) (coef ?c2))
    =>

    (assert (theorem (name T83) (coef (* 0.285 (min ?c1 ?c2)))))
    (assert (message (str-cat "T65&T80=>T83 : " (* 0.285 (min ?c1 ?c2)) " (0.285 * (min ?c1 ?c2))")))

)

(defrule rule84
    (declare (salience 50))
    (theorem (name T57) (coef ?c1)) (theorem (name T80) (coef ?c2))
    =>

    (assert (theorem (name T84) (coef (* 0.635 (min ?c1 ?c2)))))
    (assert (message (str-cat "T57&T80=>T84 : " (* 0.635 (min ?c1 ?c2)) " (0.635 * (min ?c1 ?c2))")))

)

(defrule rule85
    (declare (salience 50))
    (theorem (name T50) (coef ?c1)) (theorem (name T81) (coef ?c2))
    =>

    (assert (theorem (name T85) (coef (* 0.69 (min ?c1 ?c2)))))
    (assert (message (str-cat "T50&T81=>T85 : " (* 0.69 (min ?c1 ?c2)) " (0.69 * (min ?c1 ?c2))")))

)

(defrule rule86
    (declare (salience 50))
    (theorem (name T24) (coef ?c1)) (theorem (name T43) (coef ?c2))
    =>

    (assert (theorem (name T86) (coef (* 0.623 (min ?c1 ?c2)))))
    (assert (message (str-cat "T24&T43=>T86 : " (* 0.623 (min ?c1 ?c2)) " (0.623 * (min ?c1 ?c2))")))

)

(defrule rule87
    (declare (salience 50))
    (theorem (name T14) (coef ?c1)) (theorem (name T57) (coef ?c2))
    =>

    (assert (theorem (name T87) (coef (* 0.832 (min ?c1 ?c2)))))
    (assert (message (str-cat "T14&T57=>T87 : " (* 0.832 (min ?c1 ?c2)) " (0.832 * (min ?c1 ?c2))")))

)

(defrule rule88
    (declare (salience 50))
    (theorem (name T50) (coef ?c1)) (theorem (name T64) (coef ?c2))
    =>

    (assert (theorem (name T88) (coef (* 0.834 (min ?c1 ?c2)))))
    (assert (message (str-cat "T50&T64=>T88 : " (* 0.834 (min ?c1 ?c2)) " (0.834 * (min ?c1 ?c2))")))

)

(defrule rule89
    (declare (salience 50))
    (theorem (name T7) (coef ?c1)) (theorem (name T84) (coef ?c2))
    =>

    (assert (theorem (name T89) (coef (* 0.141 (min ?c1 ?c2)))))
    (assert (message (str-cat "T7&T84=>T89 : " (* 0.141 (min ?c1 ?c2)) " (0.141 * (min ?c1 ?c2))")))

)

(defrule rule90
    (declare (salience 50))
    (theorem (name T11) (coef ?c1)) (theorem (name T85) (coef ?c2))
    =>

    (assert (theorem (name T90) (coef (* 0.499 (min ?c1 ?c2)))))
    (assert (message (str-cat "T11&T85=>T90 : " (* 0.499 (min ?c1 ?c2)) " (0.499 * (min ?c1 ?c2))")))

)

(defrule rule91
    (declare (salience 50))
    (theorem (name T13) (coef ?c1)) (theorem (name T24) (coef ?c2))
    =>

    (assert (theorem (name T91) (coef (* 0.453 (min ?c1 ?c2)))))
    (assert (message (str-cat "T13&T24=>T91 : " (* 0.453 (min ?c1 ?c2)) " (0.453 * (min ?c1 ?c2))")))

)

(defrule rule92
    (declare (salience 50))
    (theorem (name T48) (coef ?c1)) (theorem (name T85) (coef ?c2))
    =>

    (assert (theorem (name T92) (coef (* 0.333 (min ?c1 ?c2)))))
    (assert (message (str-cat "T48&T85=>T92 : " (* 0.333 (min ?c1 ?c2)) " (0.333 * (min ?c1 ?c2))")))

)

(defrule rule93
    (declare (salience 50))
    (theorem (name T92) (coef ?c1)) (theorem (name T70) (coef ?c2))
    =>

    (assert (theorem (name T93) (coef (* 0.563 (min ?c1 ?c2)))))
    (assert (message (str-cat "T92&T70=>T93 : " (* 0.563 (min ?c1 ?c2)) " (0.563 * (min ?c1 ?c2))")))

)

(defrule rule94
    (declare (salience 50))
    (theorem (name T69) (coef ?c1)) (theorem (name T88) (coef ?c2))
    =>

    (assert (theorem (name T94) (coef (* 0.89 (min ?c1 ?c2)))))
    (assert (message (str-cat "T69&T88=>T94 : " (* 0.89 (min ?c1 ?c2)) " (0.89 * (min ?c1 ?c2))")))

)

(defrule rule95
    (declare (salience 50))
    (theorem (name T78) (coef ?c1)) (theorem (name T76) (coef ?c2))
    =>

    (assert (theorem (name T95) (coef (* 0.306 (min ?c1 ?c2)))))
    (assert (message (str-cat "T78&T76=>T95 : " (* 0.306 (min ?c1 ?c2)) " (0.306 * (min ?c1 ?c2))")))

)

(defrule rule96
    (declare (salience 50))
    (theorem (name T57) (coef ?c1)) (theorem (name T86) (coef ?c2))
    =>

    (assert (theorem (name T96) (coef (* 0.401 (min ?c1 ?c2)))))
    (assert (message (str-cat "T57&T86=>T96 : " (* 0.401 (min ?c1 ?c2)) " (0.401 * (min ?c1 ?c2))")))

)

(defrule rule97
    (declare (salience 50))
    (theorem (name T57) (coef ?c1)) (theorem (name T84) (coef ?c2))
    =>

    (assert (theorem (name T97) (coef (* 0.179 (min ?c1 ?c2)))))
    (assert (message (str-cat "T57&T84=>T97 : " (* 0.179 (min ?c1 ?c2)) " (0.179 * (min ?c1 ?c2))")))

)

(defrule rule98
    (declare (salience 50))
    (theorem (name T77) (coef ?c1)) (theorem (name T92) (coef ?c2))
    =>

    (assert (theorem (name T98) (coef (* 0.876 (min ?c1 ?c2)))))
    (assert (message (str-cat "T77&T92=>T98 : " (* 0.876 (min ?c1 ?c2)) " (0.876 * (min ?c1 ?c2))")))

)

(defrule rule99
    (declare (salience 50))
    (theorem (name T93) (coef ?c1)) (theorem (name T86) (coef ?c2))
    =>

    (assert (theorem (name T99) (coef (* 0.751 (min ?c1 ?c2)))))
    (assert (message (str-cat "T93&T86=>T99 : " (* 0.751 (min ?c1 ?c2)) " (0.751 * (min ?c1 ?c2))")))

)

(defrule rule100
    (declare (salience 50))
    (theorem (name T71) (coef ?c1)) (theorem (name T56) (coef ?c2))
    =>

    (assert (theorem (name T100) (coef (* 0.349 (min ?c1 ?c2)))))
    (assert (message (str-cat "T71&T56=>T100 : " (* 0.349 (min ?c1 ?c2)) " (0.349 * (min ?c1 ?c2))")))

)

(defrule rule101
    (declare (salience 50))
    (theorem (name T48) (coef ?c1)) (theorem (name T70) (coef ?c2))
    =>

    (assert (theorem (name T101) (coef (* 0.807 (min ?c1 ?c2)))))
    (assert (message (str-cat "T48&T70=>T101 : " (* 0.807 (min ?c1 ?c2)) " (0.807 * (min ?c1 ?c2))")))

)

(defrule rule102
    (declare (salience 50))
    (theorem (name T74) (coef ?c1)) (theorem (name T88) (coef ?c2))
    =>

    (assert (theorem (name T102) (coef (* 0.171 (min ?c1 ?c2)))))
    (assert (message (str-cat "T74&T88=>T102 : " (* 0.171 (min ?c1 ?c2)) " (0.171 * (min ?c1 ?c2))")))

)

(defrule rule103
    (declare (salience 50))
    (theorem (name T11) (coef ?c1)) (theorem (name T60) (coef ?c2))
    =>

    (assert (theorem (name T103) (coef (* 0.174 (min ?c1 ?c2)))))
    (assert (message (str-cat "T11&T60=>T103 : " (* 0.174 (min ?c1 ?c2)) " (0.174 * (min ?c1 ?c2))")))

)

(defrule rule104
    (declare (salience 50))
    (theorem (name T57) (coef ?c1)) (theorem (name T89) (coef ?c2))
    =>

    (assert (theorem (name T104) (coef (* 0.473 (min ?c1 ?c2)))))
    (assert (message (str-cat "T57&T89=>T104 : " (* 0.473 (min ?c1 ?c2)) " (0.473 * (min ?c1 ?c2))")))

)

(defrule rule105
    (declare (salience 50))
    (theorem (name T55) (coef ?c1)) (theorem (name T98) (coef ?c2))
    =>

    (assert (theorem (name T105) (coef (* 0.394 (min ?c1 ?c2)))))
    (assert (message (str-cat "T55&T98=>T105 : " (* 0.394 (min ?c1 ?c2)) " (0.394 * (min ?c1 ?c2))")))

)

(defrule rule106
    (declare (salience 50))
    (theorem (name T2) (coef ?c1)) (theorem (name T6) (coef ?c2))
    =>

    (assert (theorem (name T5) (coef (* 0.138 (min ?c1 ?c2)))))
    (assert (message (str-cat "T2&T6=>T5 : " (* 0.138 (min ?c1 ?c2)) " (0.138 * (min ?c1 ?c2))")))

)

(defrule rule107
    (declare (salience 50))
    (theorem (name T1) (coef ?c1)) (theorem (name T27) (coef ?c2))
    =>

    (assert (theorem (name T9) (coef (* 0.525 (min ?c1 ?c2)))))
    (assert (message (str-cat "T1&T27=>T9 : " (* 0.525 (min ?c1 ?c2)) " (0.525 * (min ?c1 ?c2))")))

)

(defrule rule108
    (declare (salience 50))
    (theorem (name T4) (coef ?c1)) (theorem (name T19) (coef ?c2))
    =>

    (assert (theorem (name T16) (coef (* 0.331 (min ?c1 ?c2)))))
    (assert (message (str-cat "T4&T19=>T16 : " (* 0.331 (min ?c1 ?c2)) " (0.331 * (min ?c1 ?c2))")))

)

(defrule rule109
    (declare (salience 50))
    (theorem (name T3) (coef ?c1)) (theorem (name T21) (coef ?c2))
    =>

    (assert (theorem (name T18) (coef (* 0.184 (min ?c1 ?c2)))))
    (assert (message (str-cat "T3&T21=>T18 : " (* 0.184 (min ?c1 ?c2)) " (0.184 * (min ?c1 ?c2))")))

)

(defrule rule110
    (declare (salience 50))
    (theorem (name T14) (coef ?c1)) (theorem (name T25) (coef ?c2))
    =>

    (assert (theorem (name T22) (coef (* 0.264 (min ?c1 ?c2)))))
    (assert (message (str-cat "T14&T25=>T22 : " (* 0.264 (min ?c1 ?c2)) " (0.264 * (min ?c1 ?c2))")))

)

(defrule rule111
    (declare (salience 50))
    (theorem (name T12) (coef ?c1)) (theorem (name T26) (coef ?c2))
    =>

    (assert (theorem (name T28) (coef (* 0.876 (min ?c1 ?c2)))))
    (assert (message (str-cat "T12&T26=>T28 : " (* 0.876 (min ?c1 ?c2)) " (0.876 * (min ?c1 ?c2))")))

)

(defrule rule112
    (declare (salience 50))
    (theorem (name T15) (coef ?c1)) (theorem (name T24) (coef ?c2))
    =>

    (assert (theorem (name T32) (coef (* 0.605 (min ?c1 ?c2)))))
    (assert (message (str-cat "T15&T24=>T32 : " (* 0.605 (min ?c1 ?c2)) " (0.605 * (min ?c1 ?c2))")))

)

(defrule rule113
    (declare (salience 50))
    (theorem (name T31) (coef ?c1)) (theorem (name T36) (coef ?c2))
    =>

    (assert (theorem (name T40) (coef (* 0.139 (min ?c1 ?c2)))))
    (assert (message (str-cat "T31&T36=>T40 : " (* 0.139 (min ?c1 ?c2)) " (0.139 * (min ?c1 ?c2))")))

)

(defrule rule114
    (declare (salience 50))
    (theorem (name T20) (coef ?c1)) (theorem (name T33) (coef ?c2))
    =>

    (assert (theorem (name T47) (coef (* 0.899 (min ?c1 ?c2)))))
    (assert (message (str-cat "T20&T33=>T47 : " (* 0.899 (min ?c1 ?c2)) " (0.899 * (min ?c1 ?c2))")))

)

(defrule rule115
    (declare (salience 50))
    (theorem (name T34) (coef ?c1)) (theorem (name T42) (coef ?c2))
    =>

    (assert (theorem (name T50) (coef (* 0.757 (min ?c1 ?c2)))))
    (assert (message (str-cat "T34&T42=>T50 : " (* 0.757 (min ?c1 ?c2)) " (0.757 * (min ?c1 ?c2))")))

)

(defrule rule116
    (declare (salience 50))
    (theorem (name T43) (coef ?c1)) (theorem (name T39) (coef ?c2))
    =>

    (assert (theorem (name T54) (coef (* 0.503 (min ?c1 ?c2)))))
    (assert (message (str-cat "T43&T39=>T54 : " (* 0.503 (min ?c1 ?c2)) " (0.503 * (min ?c1 ?c2))")))

)

(defrule rule117
    (declare (salience 50))
    (theorem (name T45) (coef ?c1)) (theorem (name T49) (coef ?c2))
    =>

    (assert (theorem (name T66) (coef (* 0.825 (min ?c1 ?c2)))))
    (assert (message (str-cat "T45&T49=>T66 : " (* 0.825 (min ?c1 ?c2)) " (0.825 * (min ?c1 ?c2))")))

)

(defrule rule118
    (declare (salience 50))
    (theorem (name T52) (coef ?c1)) (theorem (name T60) (coef ?c2))
    =>

    (assert (theorem (name T77) (coef (* 0.145 (min ?c1 ?c2)))))
    (assert (message (str-cat "T52&T60=>T77 : " (* 0.145 (min ?c1 ?c2)) " (0.145 * (min ?c1 ?c2))")))

)

(defrule rule119
    (declare (salience 50))
    (theorem (name T38) (coef ?c1)) (theorem (name T56) (coef ?c2))
    =>

    (assert (theorem (name T82) (coef (* 0.879 (min ?c1 ?c2)))))
    (assert (message (str-cat "T38&T56=>T82 : " (* 0.879 (min ?c1 ?c2)) " (0.879 * (min ?c1 ?c2))")))

)

(defrule rule120
    (declare (salience 50))
    (theorem (name T58) (coef ?c1)) (theorem (name T61) (coef ?c2))
    =>

    (assert (theorem (name T93) (coef (* 0.797 (min ?c1 ?c2)))))
    (assert (message (str-cat "T58&T61=>T93 : " (* 0.797 (min ?c1 ?c2)) " (0.797 * (min ?c1 ?c2))")))

)