# TOP_DOWN

Тестовое задание. Выполнено на Unity 2021.1.6f1 (64-bit)
Описание:
Основная сцена - MainScene;
Управление:
Левая КМ - передвижение / взаимодействие с объектом на близком расстоянии
Правая КМ - нанесение своему персонажу урон по 10 единиц


Сундуки - объекты:
Взаимодействие с сундуками требует определенный уровень игрока, об этом можно прочитать в описании объекта при наведении курсора на сундук.
Есть три типа сундука, которые по-разному себя ведут, когда с ними взаимодействуешь:
1) даёт опыт и позволяет получать уровень персонажу.
2) нанесет урон игроку, урон составляет 20 единиц.
3) сам себя уничтожит, если на него нажать определенное количество раз, указанное в описании.
У каждого свойства есть своё визуальное оформление, опыт - фиолетовый, урон - красный, разрушение - зелено голубой, 
их можно будет увидеть если подойти близко к сундуку.
ПРИМЕЧАНИЕ: Каждый раз сундуки генерируются новые и свойства будут разные, кроме сундука слева от персонажа при старте игры, 
он всегда даёт опыт и требует 1 уровень игрока для взаимодействия, для того чтобы быстрее получить уровень. 
Сундукам я оставил многоразовое взаимодействие, так что, если вы уже открыли сундук вы всё ещё можете нажимать на него.

Порталы:
Подойдите к нему вплотную, и он телепортирует вас на другую сцену, каждый переход инициирует вызов новых сундуков из JSON

Интерфейс:
Снизу слева - зеленый круг, уровень здоровья
Середина низ - полоска опыта с информацией о текущем уровне

Комментарии к некоторым методам я не писал, потому что старался максимально понятно называть каждые переменные и методы, чтобы сразу понимать, что они делают.

Тестирование делал на мониторе 1920 на 1080, оно же и рекомендуемое разрешение для игры.
