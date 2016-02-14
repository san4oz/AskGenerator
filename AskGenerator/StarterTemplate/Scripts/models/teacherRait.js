function TeacherRating(option, teacher, $http) {
    var self = this;
    self.id = teacher.id;
    self.name = teacher.name;
    self.image = teacher.image;
    self.value = "";
    self.points = [];
    for (var val in teacher.status) {
        if (teacher.status[val].id == option) {
            self.value = teacher.status[val].value;
        }
    }
    self.revoute = self.value ? true : false;
    for (var i = 0; i < 10; i++) {
        self.points[i] = { 'checked': self.value - 1 == i }
    }
    self.enableRevote = function () {
        self.revoute = false;
    };
    self.vote = function (option, $index, token) {
        var data = { id: self.id, questionId: option, answer: $index + 1, '__RequestVerificationToken': token };
        self.revoute = true;
        $http.post('/home/addAnswer', data).then(function () {
            self.value = $index + 1;
        }, function () {
            self.revoute = false;
            console.log('Woooops, something going wrong');
        });

    }
}