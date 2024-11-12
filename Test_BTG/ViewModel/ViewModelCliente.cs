using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Test_BTG.Model;

namespace Test_BTG.ViewModel
{
    public class ViewModelCliente : INotifyPropertyChanged
    {
        public ObservableCollection<Clientes> Clientes { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand ExcluirCommand { get; }

        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int Idade { get; set; }
        public string Endereco { get; set; }

        public ViewModelCliente()
        {
            Clientes = new ObservableCollection<Clientes>();
            SaveCommand = new Command(SaveCliente);
            CancelCommand = new Command(Cancel);
            EditarCommand = new Command<Clientes>(EditarCliente);
            ExcluirCommand = new Command<Clientes>(ExcluirCliente);
        }

        private Clientes _clienteEmEdicao;
        public Clientes ClienteEmEdicao
        {
            get => _clienteEmEdicao;
            set
            {
                _clienteEmEdicao = value;
                OnPropertyChanged(nameof(ClienteEmEdicao));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void SaveCliente()
        {
            if (string.IsNullOrEmpty(Nome) || string.IsNullOrEmpty(Sobrenome) || Idade <= 0 || string.IsNullOrEmpty(Endereco))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Favor Preencher todos os campos!", "OK");
            return;
            }

            var cliente = new Clientes
            {
                Name = Nome,
                Lastname = Sobrenome,
                Age = Idade,
                Address = Endereco
            };

            Clientes.Add(cliente);
            ClearFields();
            await Application.Current.MainPage.DisplayAlert("Sucesso", "Cliente Salvo com Sucesso!", "OK");


        }

        public void Cancel()
        {
            ClearFields();
        }

        private void ClearFields()
        {
            Nome = string.Empty;
            Sobrenome = string.Empty;
            Idade = 0;
            Endereco = string.Empty;

            OnPropertyChanged(nameof(Nome));
            OnPropertyChanged(nameof(Sobrenome));
            OnPropertyChanged(nameof(Idade));
            OnPropertyChanged(nameof(Endereco));
        }


        public async void EditarCliente(Clientes cliente)
        {
            // Aqui eu guardo os nomes Originais.
            string nomeOriginal = cliente.Name;
            string sobrenomeOriginal = cliente.Lastname;
            int idadeOriginal = cliente.Age;
            string enderecoOriginal = cliente.Address;

            // Exibo os textos, para que o usuario facilite, lembrando que isso é um teste, se fosse direto no banco, poderia fazer diversas formas. 
            string resultNome = await Application.Current.MainPage.DisplayPromptAsync("Editar Cliente", "Nome", initialValue: nomeOriginal);
            string resultSobrenome = await Application.Current.MainPage.DisplayPromptAsync("Editar Cliente", "Sobrenome", initialValue: sobrenomeOriginal);
            string resultIdade = await Application.Current.MainPage.DisplayPromptAsync("Editar Cliente", "Idade", initialValue: idadeOriginal.ToString());
            string resultEndereco = await Application.Current.MainPage.DisplayPromptAsync("Editar Cliente", "Endereço", initialValue: enderecoOriginal);


            cliente.Name = resultNome ?? cliente.Name;
            cliente.Lastname = resultSobrenome ?? cliente.Lastname;
            cliente.Age = int.TryParse(resultIdade, out int idade) ? idade : cliente.Age;
            cliente.Address = resultEndereco ?? cliente.Address;

            // Isso vai notificar todas as alterações que foram feita
            OnPropertyChanged(nameof(cliente.Name));        
            OnPropertyChanged(nameof(cliente.Lastname));   
            OnPropertyChanged(nameof(cliente.Age));   
            OnPropertyChanged(nameof(cliente.Address)); 


            await Application.Current.MainPage.DisplayAlert("Sucesso", "Cliente editado com sucesso!", "OK");
        }




        public async void ExcluirCliente(Clientes cliente)
        {
            Clientes.Remove(cliente);
            await Application.Current.MainPage.DisplayAlert("Sucesso", "Cliente removido com sucesso!", "OK");
        }
    }
}
