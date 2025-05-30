using GestorBiblioteca.Core.Enums;
using GestorBibliotecaApplication.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Notification
{
    public class EmprestimoCreatedNotification: INotification
    {
        public EmprestimoCreatedNotification(int idEmprestimo)
        {
            IdEmprestimo = idEmprestimo;

        }

        public int IdEmprestimo { get; private set; }

    }
}

public class EmprestimoCreatedNotificationHandler : INotificationHandler<EmprestimoCreatedNotification>
{
    public Task Handle(EmprestimoCreatedNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"✅ Notificação: Empréstimo #{notification.IdEmprestimo} criado com sucesso.");
        return Task.CompletedTask;
    }
}
