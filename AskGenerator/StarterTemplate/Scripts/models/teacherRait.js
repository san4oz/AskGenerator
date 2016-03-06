function TeacherRating(option, teacher, $http) {
    var self = this;
    self.id = teacher.id;
    self.name = teacher.name;
    self.image = teacher.image;
    self.value = '';
    self.selected = ''
    self.points = [];
    for (var val in teacher.status) {
        if (teacher.status[val].id == option) {
            self.selected = self.value = teacher.status[val].value;
            break;
        }
    }

    for (var i = 0; i < 10; i++) {
        self.points[i] = { 'checked': self.value - 1 == i }
    }

    self.vote = function (option, $index, token, success) {
        if (confirm('Change vote ?')) {
            var data = { id: self.id, questionId: option, answer: $index + 1, '__RequestVerificationToken': token };
            $http.post('/home/addAnswer', data).then(function () {
                self.selected = self.value = $index + 1;
                if (success)
                    success();
            }, function () {
                console.log('Woooops, something going wrong');
            });
        } else {
            self.mouseOut();
        }
    };

    self.mouseOver = function (index) {
        self.value = index + 1;
    };

    self.mouseOut = function () {
        self.value = self.selected;
    };
}