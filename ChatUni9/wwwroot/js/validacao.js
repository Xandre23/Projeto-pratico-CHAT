$(document).ready(function () {
	$("#frm-create-account").validate({
		rules: {
			inputNome: {
				required: true,
				maxlength: 30,
				minlength: 2

			},
			inputSobrenome: {
				required: true,
				maxlength: 30,
				minlength: 10
			},

			inputEmail: {
				required: true,
				email: true

			},
			inputPassword: {
				required: true

			}
		},
		messages: {
			inputNome: {
				required: 'Por favor, Insira seu nome!!'
			},
			inputSobrenome: {
				required: 'Por favor, Insira seu sobrenome.'
			},
			inputEmail: {
				required: 'Por favor, Insira seu email.'
			},
			inputPassword: {
				required: 'Por favor, Insira sua senha.'
			}

		}

	})
})



