using Game.Enums;

namespace Game.Models
{
    /// <summary>
    /// Manages the Message formatting for the UI to Display
    /// </summary>
    public class BattleMessagesModel
    {

        // Beginning of the Html Block for html formatting
        public const string htmlHead = @"<html><body bgcolor=""#E8D0B6""><p>";

        // Ending of the Html Block for Html formatting
        public const string htmlTail = @"</p></body></html>";

        // Name of the Attacker
        public string AttackerName = string.Empty;

        // The status of the Attack
        public string AttackStatus = string.Empty;

        // Message when something bad happens with Critical Miss
        public string BadCriticalMissMessage = string.Empty;

        // The Remaining Health Message
        public int CurrentHealth;

        // Amount of Damage
        public int DamageAmount;

        // Message when something Drops
        public string DroppedMessage = string.Empty;

        // Turn Experience Earned Message
        public string ExperienceEarned = string.Empty;

        // The Status of the action
        public HitStatusEnum HitStatus = HitStatusEnum.Unknown;

        // Level Up Message
        public string LevelUpMessage = string.Empty;
        // Is the player a character or a monster
        public PlayerTypeEnum PlayerType = PlayerTypeEnum.Unknown;

        // Name of who the target was
        public string TargetName = string.Empty;

        // Turn Message
        public string TurnMessage = string.Empty;

        // Turn Special Message
        public string TurnMessageSpecial = string.Empty;


        public void ClearMessages()
        {
            PlayerType = PlayerTypeEnum.Unknown;
            HitStatus = HitStatusEnum.Unknown;
            AttackerName = string.Empty;
            TargetName = string.Empty;
            AttackStatus = string.Empty;
            TurnMessage = string.Empty;
            TurnMessageSpecial = string.Empty;
            ExperienceEarned = string.Empty;
            LevelUpMessage = string.Empty;
            BadCriticalMissMessage = string.Empty;

            DamageAmount = 0;
            CurrentHealth = 0;
        }

        /// <summary>
        /// Return formatted string
        /// </summary>
        /// <returns></returns>
        public string GetSwingResult() => HitStatus.ToMessage();

        /// <summary>
        /// Return formatted Damage
        /// </summary>
        /// <returns></returns>
        public string GetDamageMessage() => $" for {DamageAmount} damage ";

        /// <summary>
        /// Returns the String Attacker Hit Defender
        /// </summary>
        /// <returns></returns>
        public string GetTurnMessage() => AttackerName + GetSwingResult() + TargetName;

        /// <summary>
        /// Remaining Health Message
        /// </summary>
        /// <returns></returns>
        public string GetCurrentHealthMessage() => " remaining health is " + CurrentHealth.ToString();

        /// <summary>
        /// Returns a blank HTML page, used for clearing the output window
        /// </summary>
        /// <returns></returns>
        public string GetHtmlBlankMessage()
        {
            const string myResult = htmlHead + htmlTail;
            return myResult;
        }

        /// <summary>
        /// Output the Turn as a HTML string
        /// </summary>
        /// <returns></returns>
        public string GetHtmlFormattedTurnMessage()
        {
            var attackerStyle = @"<span style=""color:blue"">";
            var defenderStyle = @"<span style=""color:green"">";

            if (PlayerType == PlayerTypeEnum.Monster)
            {
                // If monster, swap the colors
                defenderStyle = @"<span style=""color:blue"">";
                attackerStyle = @"<span style=""color:green"">";
            }

            var swingResult = string.Empty;
            switch (HitStatus)
            {
                case HitStatusEnum.Miss:
                    swingResult = @"<span style=""color:yellow"">";
                    break;

                case HitStatusEnum.CriticalMiss:
                    swingResult = @"<span bold style=""color:yellow; font-weight:bold;"">";
                    break;

                case HitStatusEnum.CriticalHit:
                    swingResult = @"<span bold style=""color:red; font-weight:bold;"">";
                    break;

                case HitStatusEnum.Hit:
                default:
                    swingResult = @"<span style=""color:red"">";
                    break;
            }

            var htmlBody = string.Empty;
            htmlBody += $@"{attackerStyle}{AttackerName}</span>";
            htmlBody += $@"{swingResult}{GetSwingResult()}</span>";
            htmlBody += $@"{defenderStyle}{TargetName}</span>";
            htmlBody += $@"<span>{TurnMessageSpecial}</span>";

            var myResult = htmlHead + htmlBody + htmlTail;
            return myResult;
        }
    }
}
