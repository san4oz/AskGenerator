function TeacherRating(option, teacher, $http) {
    var self = this;
    self.id = teacher.id;
    self.name = teacher.name;
    self.image = teacher.image;
    self.value = -1;
    for (var val in teacher.status) {
        if (teacher.status[val].id == option) {
            self.value = teacher.status[val].value ? teacher.status[val].value : -1;
        }
    }
    self.readonly = self.value > 0 ? true : false;
    self.vote = function (option, $index, token) {
        var data = { id: self.id, questionId: option, answer: $index, '__RequestVerificationToken': token };
        self.readonly = true;
        $http.post('/home/addAnswer', data).then(function () {
            self.value = $index;
        }, function () {
            self.revoute = false;
            console.log('Woooops, something going wrong');
        });

    }
}